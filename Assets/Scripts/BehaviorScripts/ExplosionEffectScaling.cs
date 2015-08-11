using UnityEngine;
using System.Collections;

public class ExplosionEffectScaling : MonoBehaviour {

    public SpriteRenderer thing;
    public GameObject parentThing;



    public float SpinMax;
    private float spin;

	// Use this for initialization
	void Start () {
        thing = this.GetComponent<SpriteRenderer>();

        spin = Random.Range(SpinMax, -SpinMax);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, spin);
        float a = parentThing.gameObject.transform.localScale.x;
        thing.color = new Color(1, 1, 1, a);
	}
}
