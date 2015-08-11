using UnityEngine;
using System.Collections;

public class SoundVarianceScript : MonoBehaviour {


    public AudioSource clip;
    public float variance;

	// Use this for initialization
	void Start () {
        clip.pitch = Random.Range(1 + variance, 1 - variance);
        clip.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
