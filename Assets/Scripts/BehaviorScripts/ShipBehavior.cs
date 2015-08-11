using UnityEngine;
using System.Collections;

public class ShipBehavior : MonoBehaviour {

    public float targetX = 14f;
    public float targetY = 0f;
    public float step;
    public Vector3 target;
    public bool safe = false;
    public GameObject Explosion;
    private int points = -1000;
    public float givenX;

	// Use this for initialization
	void Start () {
        targetY += Random.Range(-2, 2);
        var temp = transform.position;
        temp.y = -6f;
        temp.x = givenX * .2f;
        temp.z = 0;
        transform.position = temp;
        target = new Vector3(temp.x, targetY, 0);
	}
	
	// Update is called once per frame
	void Update () {
        step += .003f * Time.deltaTime;

        transform.LookAt(target);

        if (target.x <= targetX)
        {
            var temp = target; 
            temp.x = target.x + (2 * Time.deltaTime);
            target = temp;
        }
        if (transform.position.x >= 13.5f)
        {
            points = 1000;
            safe = true;
            Explode();
        }

        transform.position = Vector3.MoveTowards(transform.position, target, step);
        var tempR = transform.rotation;
        tempR.x = 0;
        tempR.y = 0;
        transform.rotation = tempR;
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            Explode();
        }
    }

    public void Explode()
    {
        var pos = this.transform.position;
        pos.z = 0;

        var Explode = Instantiate(Explosion, pos, Quaternion.identity) as GameObject;
        var giveExplode = Explode.GetComponent<ExplosionParent>();
        if (points == 10000) giveExplode.sizeMulitplyer = 0;
        else giveExplode.sizeMulitplyer = .3f;
        giveExplode.chain = 1;
        giveExplode.PointBase = points;

        Destroy(this.gameObject);
    }

}
