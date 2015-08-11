using UnityEngine;
using System.Collections;

public class Debris : MonoBehaviour {

    public Vector3 target;
    public float velocity;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = 15;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target, velocity);
        float dis = Vector3.Distance(this.transform.position, target);
        transform.LookAt(target);
        var temp = transform.rotation;
        temp.x = 0;
        temp.y = 0;
        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(this.gameObject);
        transform.rotation = temp;
	}
}
