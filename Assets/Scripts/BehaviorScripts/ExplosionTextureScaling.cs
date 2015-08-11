using UnityEngine;
using System.Collections;

public class ExplosionTextureScaling : MonoBehaviour {

    public GameObject parent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        renderer.material.mainTextureScale = new Vector2(parent.transform.localScale.x, parent.transform.localScale.y);
	}
}
