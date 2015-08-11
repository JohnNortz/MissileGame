using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonitorSequenceScript : MonoBehaviour {

    public RawImage Dust;
    public Texture texture0;
    public Color color0;
    public Texture texture1;
    public Color color1;
    public Texture texture2;
    public Color color2;
    public float startDelay;
    public float transitionTime;
    private float behaviorTimer;

	// Use this for initialization
	void Start () {
        Dust.texture = texture0;
        Dust.color = color0;
        behaviorTimer = (transitionTime * 3) + startDelay;
	}
	
	// Update is called once per frame
	void Update () {
        behaviorTimer -= Time.deltaTime;

        if (behaviorTimer <= transitionTime)
        {
            Dust.texture = texture2;
            float a = (behaviorTimer/transitionTime);
            var colorA = color2;
            color2.a = a;
            Dust.color = colorA;
        }
        else if (behaviorTimer <= transitionTime * 2)
        {
            Dust.texture = texture2;
            Dust.color = color2;
        }
        else if (behaviorTimer <= transitionTime * 3)
        {
            Dust.texture = texture1;
            Dust.color = color1;
        }

        if (behaviorTimer < 0) Destroy(this.gameObject);
	
	}
}
