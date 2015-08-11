using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour {

    public int chain;

    public float MaxSize;
    public float TimeToFull;

    private bool Up;
    private float rate;

    public float SpinMax;
    private float spin;



	// Use this for initialization
	void Start () {
        Up = true;
        spin = Random.Range(SpinMax, -SpinMax);
	}


	
	// Update is called once per frame
	void Update () {
        rate = MaxSize / TimeToFull;
        transform.Rotate(0, 0, spin);
        if (Up)
        {
            var currentScale = this.gameObject.transform.localScale.x;
            currentScale += rate * 5 * Time.deltaTime;
            this.gameObject.transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
            if (.2f * this.gameObject.transform.localScale.x >= MaxSize)
            {
                Up = false;
            }
        }
        else
        {
            var currentScale = this.gameObject.transform.localScale.x;
            var _rate = rate;
            if (transform.position.y > 0) _rate = rate / (1 + (transform.position.y * .5f));
            currentScale -= _rate * .5f * Time.deltaTime;
            this.gameObject.transform.localScale = new Vector3 (currentScale, currentScale, currentScale);
            if (.2f  * this.gameObject.transform.localScale.x <= .01f)
            {
                Destroy(transform.parent.gameObject);
            }
        }
	}
}
