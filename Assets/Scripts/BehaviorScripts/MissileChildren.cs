using UnityEngine;
using System.Collections;

public class MissileChildren : MonoBehaviour {

    public GameObject parent;
    public Vector3 parentTarget;
    public Vector3 offset;
    public Vector3 _offset;
    public Vector3 target;
    public float sizeMultiplyer = .4f;
    public float _sizeMultiplyer = .4f;
    private float defaultPlayerVelocity = .15f;
    public float step;
    private float _step;
    public GameObject Explosion;

	// Use this for initialization
	void Start () {
        float offMult = 1;
        if (parentTarget.y >= 0)
        {
            offMult = 1 / (1 + (parentTarget.y * .75f));
        }

        offset = new Vector3(Random.Range(-2 * offMult, 2 * offMult), Random.Range(-2 * offMult, 2 * offMult), 0);
        target = parent.transform.position + offset;

        this.name = "Missile";
        this.tag = "Missile";
        target.z = 0;
	    
	}
	
	// Update is called once per frame
	void Update () {

        var _parent = parent;

        if (_parent == null)
        {
            Explode(0);
        }
        
        
        if (target != null)
        {
            target = new Vector3(0, 0, 0);
            target = (_parent.transform.position + offset);
        }
        if (transform.position.y >= 0)
        {
            _step = step / (1 + (transform.position.y * .75f));
            _sizeMultiplyer = sizeMultiplyer / ((transform.position.y * .333f) + 1);
        }
        else
        {
            _step = step;
            _sizeMultiplyer = sizeMultiplyer;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, _step);
        float dis = Vector3.Distance(this.transform.position, target);


        
	}
    public void Explode(int Chain)
    {
        var pos = this.transform.position;
        pos.z = 0;

        var Explode = Instantiate(Explosion, pos, Quaternion.identity) as GameObject;
        var giveExplode = Explode.GetComponent<ExplosionParent>();
        giveExplode.chain = Chain + 1;
        giveExplode.sizeMulitplyer = _sizeMultiplyer;
        giveExplode.PointBase = 0;


        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            var hitThing = collision.gameObject.GetComponent<ExplosionScript>();
            Explode(hitThing.chain);
        }
    }
}
