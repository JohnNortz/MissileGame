using UnityEngine;
using System.Collections;

public class backgroundScroller : MonoBehaviour {

    public float motion;

    void Update()
    {
        transform.Translate(Vector3.left * motion * Time.deltaTime);
        if (transform.position.x <=-35.78f)
        {
            var temp = transform.position;
            temp.x = 27.78f;
            transform.position = temp;
        }
    }
}
