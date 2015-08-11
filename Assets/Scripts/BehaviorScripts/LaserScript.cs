using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    public float timer = 1f;

	// Use this for initialization
    void Start()
    {
        transform.Rotate(0, 0, Random.Range(-135f, -45f)); 
        Debug.Log("Laser Rotation Target: " + transform.rotation.z.ToString());
	}
	
	// Update is called once per frame
	void Update () {

        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(this.gameObject);
	}
}
