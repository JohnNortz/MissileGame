using UnityEngine;
using System.Collections;

public class PointerSweep : MonoBehaviour {

    private GameObject[] pointers;
    public float y = 6;
    public float yMax;
    public float sweepSpeed;
    public GameObject sweepObj;
	
	// Update is called once per frame
	void Update () {
        pointers = new GameObject[100];
        pointers = GameObject.FindGameObjectsWithTag("MissileTag");
        y += sweepSpeed * Time.deltaTime;
        if (y > yMax) y = -6F;
        else
        {
            foreach (GameObject pointer in pointers)
            {
                if (pointer.transform.position.y < y && pointer.transform.position.y > y - 1)
                {
                    pointer.GetComponent<Pointer>().Reset();
                }
            }
        }
        Vector3 temp = sweepObj.transform.position;
        temp.y = y;
        sweepObj.transform.position = temp;
	}
}
