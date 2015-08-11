using UnityEngine;
using System.Collections;

public class DetailMotionScript : MonoBehaviour {

    public float motion;
    public bool manual = false;

	// Use this for initialization
	void Start () {
        if (!manual)
        {
            float offset = Random.Range(0, .2f);
            motion = .8f - offset;
            var temp = transform.position;
            var tempS = transform.localScale;
            temp.y += offset * 2;
            temp.z = 13 + (offset * 35);
            tempS.x = tempS.x - offset;
            tempS.y = tempS.y - offset;

            transform.position = temp;
            transform.localScale = tempS;
        }
        if(motion == null)
        {
            motion = 100;
        }
	}
	
	// Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * motion * Time.deltaTime);
        if (transform.position.x < -20)
        {
            Destroy(this.gameObject);
        }
	}
}
