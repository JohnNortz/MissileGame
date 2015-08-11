using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MissileScript : MonoBehaviour {

    public float velocity;
    public float PointBase;
    public string type;

    public bool playerControl;
    public Vector3 target;
    public GameObject Explosion;
    public GameObject SupplyBox;
    public Camera WaveManagerObj;
    public float sizeMultiplyer = 1;
    public float _sizeMultiplyer = 1;
    public float waveVeloBonus;
    private float defaultPlayerVelocity = .15f;
    private float defaultVelocity = .01f;
    private float startDis;
    private float step;
    private Vector3 secondaryTargetPos;
    private Vector3 primaryTargetPos;
    private float worldScreenHeight; 
    private float worldScreenWidth;
    private bool dirSwitch;
    private float behaviorTimer;
    private float splitIntoNumber;
    private int counter;
    private int wave;
    public float[] Upgrades;
    public bool Upgrade0 = false;
    public bool Upgrade1 = false;
    public LineRenderer lineRenderer;
    public GameObject ClusterSplit;

    public GameObject PointerObj;
    private float trailTimer = .75f;

    public GameObject upgradeChildMissile;

	// Use this for initialization
	void Start () {
        worldScreenHeight = (float)Camera.main.orthographicSize;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        sizeMultiplyer = 1f;
        target.z = 0;
        startDis = Vector3.Distance(this.transform.position, target);

        var Pointer = Instantiate(PointerObj, this.transform.position, Quaternion.identity) as GameObject;
        Pointer.GetComponent<Pointer>().target = this.transform;
        
        WaveManagerObj = GameObject.Find("Main Camera").camera;
        var WaveManager = WaveManagerObj.GetComponent<WaveManager>();
        Color a = new Color(1, 0, 0, 1);

        if (playerControl)
        {
            Pointer.GetComponent<Pointer>().targetPos = target;
            Upgrades = WaveManagerObj.GetComponent<PlayerManager>().Upgrades;

            if (Upgrades[0] > 0)
            {
                Upgrade1 = true;
                Destroy(this.gameObject.GetComponent<CircleCollider2D>());
            }
            if (Upgrades[1] > 0) Upgrade1 = true;

            if (playerControl)
            {
                if (Upgrades[2] > 0)
                {
                    sizeMultiplyer += (Upgrades[2] * .15f);
                }

            }
        }
        if (!playerControl) this.gameObject.GetComponent<TrailRenderer>().enabled = false;


        if(!playerControl) wave = WaveManager.wave;
        else
        {
            if(lineRenderer != null && type == "Flak") lineRenderer.SetColors( a, a);
        }

        switch (type)
        {
            case "default":
                velocity = defaultVelocity;
                if (playerControl) velocity = defaultPlayerVelocity;
                PointBase = 20f;
                break;
            case "Sidewinder":
                velocity = .012f;
                PointBase = 50f;
                primaryTargetPos = target;
                primaryTargetPos.x = primaryTargetPos.x + (worldScreenWidth * .25f);
                secondaryTargetPos = target;
                secondaryTargetPos.x = secondaryTargetPos.x - (worldScreenWidth * .25f);
                counter = 0;
                break;
            case "BigExplosion":
                velocity = .01f;
                sizeMultiplyer = 1.75f;
                PointBase = 20f;
                if (playerControl)
                {
                    sizeMultiplyer = 2f;
                    velocity = defaultPlayerVelocity * .7f;
                    PointBase = 0;
                }
                break;

            case "Fast":
                velocity = .025f;
                PointBase = 40f;
                sizeMultiplyer = .75f;
                break;
            case "Rock":
                velocity = .008f;
                PointBase = 5f;
                break;
            case "ClusterRockets":
                velocity = .012f;
                PointBase = 100f;
                splitIntoNumber = 1f;
                splitIntoNumber += Mathf.Floor((wave / 3));
                behaviorTimer = 7 - wave;
                break;
            case "Supply":
                PointBase = -100f;
                sizeMultiplyer = 0f;
                velocity = .015f;
                target.x = Random.Range(8, 0);
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
                transform.Find("SupplyTrail").GetComponent<TrailRenderer>().enabled = true;
                this.gameObject.GetComponent<TrailRenderer>().enabled = false;
                break;
            case "Upgrade":
                PointBase = -100f;
                sizeMultiplyer = 0f;
                velocity = .015f;
                target.x = Random.Range(8, 0);
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
                transform.Find("SupplyTrail").GetComponent<TrailRenderer>().enabled = true;
                this.gameObject.GetComponent<TrailRenderer>().enabled = false;
                break;
            case "Flak":
                PointBase = 0f;
                velocity = .05f;
                sizeMultiplyer = .26f;
                this.gameObject.GetComponent<LineRenderer>().enabled = false;
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
                playerControl = false;
                break;
            default:
                break;


        }

        if (!playerControl) velocity = velocity + (waveVeloBonus * wave);

        this.name = "Missile";
        this.tag = "Missile";
        target.z = 0;
        //transform.LookAt(target);
        //var temp = transform.rotation;
        //temp.x = 0;
        //temp.y = 0;
        //transform.rotation = temp;
        //startDis = Vector3.Distance(this.transform.position, target);
        step = velocity;

	}


	
	// Update is called once per frame
	void Update () {
        float _step = step;
        if (transform.position.y >= 0)
        {
            _step = step / (1 + (transform.position.y * .375f));    //fuck with this to change scaleing shit
            _sizeMultiplyer = sizeMultiplyer / ((transform.position.y * .375f) + 1);
        }
        else
        {
            _step = step;
            _sizeMultiplyer = sizeMultiplyer;
        }
        if (transform.position.y <= 1.33 && !playerControl && type != "Supply" && type != "Upgrade")   //this should be the blue lines height
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
        }
        transform.position = Vector3.MoveTowards(transform.position, target, _step);
        float dis = Vector3.Distance(this.transform.position, target);
        if (playerControl && dis <= startDis*.35 && Upgrade1)
        {
            if (Upgrades[1] > 0)
            {
                for(int i = 0; i <= Upgrades[1]; i++)
                {
                    var Child = Instantiate(upgradeChildMissile, this.transform.position, Quaternion.identity) as GameObject;
                    var giveChild = Child.GetComponent<MissileChildren>();
                    giveChild.parent = this.gameObject;
                    giveChild.parentTarget = target;
                    giveChild.step = step * 2f;
                }
                Upgrade1 = false;
            }
        }


        Debug.DrawRay(this.transform.position, (target - this.transform.position), Color.red);
        if (dis <= .1f)
        {
            Explode(0);
        }

        switch (type)
        {
            case "Sidewinder":
                float lerpSpeed = 2f;                                    // creates 2 vector3s that the target the toggles back and forth between
                if (behaviorTimer <= 0 && counter < wave)
                {
                    behaviorTimer = lerpSpeed;
                    dirSwitch = !dirSwitch;
                }
                if (!dirSwitch && dis >= 10f) target = primaryTargetPos;
                if (dirSwitch && dis >= 10f) target = secondaryTargetPos;
                
                break;


            case "ClusterRockets":
                if (behaviorTimer <= 0)
                {
                    for (int n = (int)splitIntoNumber; n >= 0; n--)
                    {
                        Vector3 startPos = this.transform.position;
                        var missileAway = Instantiate(ClusterSplit, startPos, Quaternion.identity) as GameObject;
                        missileAway.GetComponent<MissileScript>().target = new Vector3(Random.Range(worldScreenWidth, -worldScreenWidth), -worldScreenHeight, 0);
                        missileAway.GetComponent<MissileScript>().type = "default";
                        missileAway.GetComponent<SpriteRenderer>().enabled = true;
                        missileAway.gameObject.GetComponent<TrailRenderer>().enabled = false;
                    }

                    Destroy(this.gameObject);

                }
                break;
            default:
                break;
        }
        if (behaviorTimer <= 0)
        {
            //transform.LookAt(target);
            var temp = transform.rotation;
            temp.x = 0;
            temp.y = 0;
            transform.rotation = temp;
        }

        if (behaviorTimer > 0) behaviorTimer -= Time.deltaTime;
        if (trailTimer > 0) trailTimer -= Time.deltaTime;
        else if (trailTimer > -1 && !playerControl)
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = true;
            trailTimer = -2;
        }
        if (playerControl)
        {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target);
        }
	}



    public void Explode(int Chain) {
        var pos = this.transform.position;
        pos.z = 0;

        var Explode = Instantiate(Explosion, pos, Quaternion.identity) as GameObject;
        var giveExplode = Explode.GetComponent<ExplosionParent>();
        giveExplode.chain = Chain + 1;
        giveExplode.sizeMulitplyer = _sizeMultiplyer; 
        if (!playerControl) giveExplode.PointBase = PointBase;


        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            var hitThing = collision.gameObject.GetComponent<ExplosionScript>();
            Explode(hitThing.chain);
        }
        if (collision.gameObject.tag == "Laser")
        {
            var hitThing = collision.gameObject.GetComponent<LaserScript>();
            Explode(0);
        }
        if (collision.gameObject.tag == "Ground" && !playerControl)
        {
            var HitCheck = WaveManagerObj.GetComponent<HitBoxSizeScript>();
            if (this.transform.position.x <= HitCheck.bumperHalfRight.transform.position.x && this.transform.position.x >= HitCheck.bumperHalfLeft.transform.position.x)   //CHECK if within Hitbox Half then Full
            {
                if (type == "Supply")
                {
                    if (this.transform.position.x >= WaveManagerObj.GetComponent<GameManager>().PlayerMissileSpawnLocation.x) 
                    {
                        var pos = new Vector3(0, -9.1f, 2);
                        pos.x = ((this.transform.position.x - WaveManagerObj.GetComponent<GameManager>().PlayerMissileSpawnLocation.x) * 5f);
                        var Supply = Instantiate(SupplyBox, pos, Quaternion.identity) as GameObject;

                        PointBase = 100;
                    }
                }
                else if (type == "Upgrade")
                {
                    if (this.transform.position.x >= WaveManagerObj.GetComponent<GameManager>().PlayerMissileSpawnLocation.x)
                    {
                        var pos = new Vector3(0, -9.1f, 2);
                        pos.x = ((this.transform.position.x - WaveManagerObj.GetComponent<GameManager>().PlayerMissileSpawnLocation.x) * 5f);
                        var Supply = Instantiate(SupplyBox, pos, Quaternion.identity) as GameObject;
                        int coin = (Random.Range(0, 20));
                        if (coin <= 4) Supply.GetComponent<SupplyBoxMotion>().type = "Turret";
                        if (coin > 4 && coin <= 6) Supply.GetComponent<SupplyBoxMotion>().type = "Blast Shield";
                        if (coin > 6 && coin <= 12) Supply.GetComponent<SupplyBoxMotion>().type = "Blast Radius";
                        if (coin > 12 && coin <= 19) Supply.GetComponent<SupplyBoxMotion>().type = "Children Missiles";
                        if (coin == 20) Supply.GetComponent<SupplyBoxMotion>().type = "Hard Shell";
                        PointBase = 100;
                    }
                }
                else if (this.transform.position.x <= HitCheck.bumperFullRight.transform.position.x && this.transform.position.x >= HitCheck.bumperFullLeft.transform.position.x)   //CHECK if within Hitbox Half then Full
                {
                    WaveManagerObj.GetComponent<PlayerManager>().TakeDamage(2);
                    PointBase = 0;
                }
                else
                {
                    WaveManagerObj.GetComponent<PlayerManager>().TakeDamage(1);
                    PointBase = 0;
                }
                
            }

            PointBase = PointBase * 10;
            Explode(0);
        }
    }
}
