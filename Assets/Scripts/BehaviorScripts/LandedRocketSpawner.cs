using UnityEngine;
using System.Collections;

public class LandedRocketSpawner : MonoBehaviour {

    //uses an array of launch times to determin random starting locations

    public int totalObjects;
    public float totalTime;
    public GameObject objectsToSpawn;
    private float[] _objects;
    private float timeElapsed = 0;
    private bool Going = false;

	void Start () {

	}


    void Update()
    {

        if (Going) timeElapsed += Time.deltaTime;
        if (timeElapsed >= totalTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void SpawnRockets()
    {
        timeElapsed = 0;
        _objects = new float[totalObjects];
        for (int i = 0; i < totalObjects; i++)
        {
            _objects[i] = Random.Range(0, totalTime);

        }
        for (int i = 0; i < totalObjects; i++)
        {
            var pos = this.transform.position;
            pos.x = pos.x + (_objects[i] * 2);
            var rocket = Instantiate(objectsToSpawn, pos, Quaternion.identity) as GameObject;


        }
        Going = true;
    }
}
