using UnityEngine;
using System.Collections;

public class OnSpawnRenderDelay : MonoBehaviour {

    public SpriteRenderer sprite;
    public AudioSource clip;
    public float timer;
    public bool active = false;
	
	void Start()
    {
        timer += Random.Range(.3f, -.3f);
    }
    
    // Update is called once per frame
	void Update () {
        if (timer > 0)
        {
            sprite.enabled = false;
            timer -= Time.deltaTime;
        }
        else if (!active)
        {
            if (clip != null)
            {
                clip.pitch = Random.Range(1.1f, .9f);
                clip.Play();

            }
            sprite.enabled = true;
            timer = 0;
            active = true;
        }
	}
}
