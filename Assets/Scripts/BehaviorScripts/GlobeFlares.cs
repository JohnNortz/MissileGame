using UnityEngine;
using System.Collections;

public class GlobeFlares : MonoBehaviour {

    public GameObject flare;
    private float variance = 10;
    private float[] vars;
	// Use this for initialization
	void Start () {
        vars = new float[10];
        for (int i = 0; i < 10; i++ )
        {
            vars[i] = Random.Range(10, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        variance -= Time.deltaTime;
        for (int i = 0; i < 10; i++ )
        {
            if(vars[i] != -1 && vars[i] >= variance)
            {
                var pos = this.transform.position;
                pos.x = pos.x + Random.Range(-.5f, .5f);
                pos.y = pos.y + Random.Range(-.5f, .5f);
                var flar = Instantiate(flare, pos, Quaternion.identity) as GameObject;
                vars[i] = -1;
            }
        }
	}
}
