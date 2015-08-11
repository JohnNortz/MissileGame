using UnityEngine;
using System.Collections;

public class ExpandWithExplosionScript : MonoBehaviour {
    public float MaxSize;
    public float TimeToFull;
    private float rate;
    public float SpinMax;
    private float spin;
    public SpriteRenderer renderer;


	// Use this for initialization
	void Start () {
        spin = Random.Range(SpinMax, -SpinMax);
	}
	
	// Update is called once per frame
	void Update () {
        rate = MaxSize / TimeToFull;
        var currentScale = this.gameObject.transform.localScale.x;
        currentScale += rate * Time.deltaTime;
        this.gameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);

        Color newColor = renderer.color;
        newColor.a = .5f - ((.2f * this.gameObject.transform.localScale.x) / (MaxSize * 2.33f));
        renderer.color = newColor;
        transform.Rotate(0, 0, spin);

	}
}
