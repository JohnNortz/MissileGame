using UnityEngine;
using System.Collections;

public class Pointer : MonoBehaviour {

    public Transform target;
    public Vector3 targetPos;
    public GameObject point;
    private float alphaMax = 5;
    private float alpha;
    public bool friendly;

    public Sprite default_Enemy;
    public Sprite default_Player;
    public Sprite Big_Enemy;
    public Sprite Big_Player;
    public Sprite Sidewinder;
    public Sprite Flak;
    public Sprite Upgrade;
    public Sprite Cluster_Enemy;
    public Sprite Cluster_Player;
    public Sprite supply_ammo;

    public AudioSource clip;
    private bool first = true;

    //public string typeDebug;

    void Start()
    {
            
        alpha = 0;
        if (friendly)
        {
            first = false;
            alpha = 1;
        }
        //typeDebug = target.gameObject.GetComponent<MissileScript>().type;
        switch (target.gameObject.GetComponent<MissileScript>().type)
        {
            case "Supply":
                point.GetComponent<SpriteRenderer>().sprite = supply_ammo;
                break;
            case "ClusterRockets":
                if (!friendly) point.GetComponent<SpriteRenderer>().sprite = Cluster_Enemy;
                else point.GetComponent<SpriteRenderer>().sprite = Cluster_Player;
                break;
            case "Upgrade":
                point.GetComponent<SpriteRenderer>().sprite = Upgrade;
                break;
            case "Flak":
                point.GetComponent<SpriteRenderer>().sprite = Flak;
                break;
            case "BigExplosion":
                if (!friendly) point.GetComponent<SpriteRenderer>().sprite = Big_Enemy;
                else point.GetComponent<SpriteRenderer>().sprite = Big_Player;
                break;
            case "Sidewinder":
                point.GetComponent<SpriteRenderer>().sprite = Sidewinder;
                break;
            case "default":
                if (!friendly) point.GetComponent<SpriteRenderer>().sprite = default_Enemy;
                else point.GetComponent<SpriteRenderer>().sprite = default_Player;
                break;
            default:

                break;


        }
    }
	
	// Update is called once per frame
	void Update () {
        if (target == null) Destroy(this.gameObject);
        if (target != null) transform.position = target.position;
        if (targetPos != new Vector3(0, 0, 0)) transform.position = targetPos;
        if (alpha > 1f)
        {
            alpha -= Time.deltaTime;
        }
        float a = ((alpha / 5) / 1);
        if (target != null && target.gameObject.GetComponent<MissileScript>().type == "Supply") point.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
        else point.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);

        if (first) point.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        if (friendly) point.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
	}

    public void Reset ()
    {
        alpha = alphaMax;
        if(first)
        {
            if (clip != null)clip.Play();
            first = false;
        }
    }
}
