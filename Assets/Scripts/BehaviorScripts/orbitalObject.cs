using UnityEngine;
using System.Collections;

public class orbitalObject : MonoBehaviour {


    public float velocity;
    private float worldScreenHeight;
    private float worldScreenWidth;
    public Vector3 target;
    public string type = "";
    public GameObject defaultMissile;
    public bool once = true;
    public GameObject debris;

	// Use this for initialization
    void Start()
    {
        transform.position = new Vector3(-13.5f - (Random.Range( 0, 3)), -1.3f + (Random.Range(-.5f, 2f)), 0);
        target = new Vector3(13.5f, -1.3f + (Random.Range(-.5f, 2f)), 0);
        worldScreenHeight = (float)Camera.main.orthographicSize;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, velocity);
        float dis = Vector3.Distance(this.transform.position, target);
        transform.LookAt(target);
        var temp = transform.rotation;
        temp.x = 0;
        temp.y = 0;

        transform.rotation = temp;
        Debug.DrawRay(this.transform.position, (target - this.transform.position), Color.red);
        if (transform.position.x >= 0 && once)
        {
            Vector3 startPos = transform.position;
            var missileAway = Instantiate(defaultMissile, startPos, Quaternion.identity) as GameObject;
            missileAway.GetComponent<MissileScript>().target = new Vector3(Random.Range(0, -worldScreenWidth), -worldScreenHeight, 0);
            missileAway.GetComponent<MissileScript>().type = type;
            once = false;
        }
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            var hitThing = collision.gameObject.GetComponent<ExplosionScript>();
            Explode(-1000);
        }
    }

    public void Explode(int PointBase)
    {
        var pos = this.transform.position;
        pos.z = 0;

        var Explode = Instantiate(debris, pos, Quaternion.identity) as GameObject;
        var giveExplode = Explode.GetComponent<Debris>();
        giveExplode.target = target;
        giveExplode.velocity = velocity;

        Destroy(this.gameObject);
    }
}
