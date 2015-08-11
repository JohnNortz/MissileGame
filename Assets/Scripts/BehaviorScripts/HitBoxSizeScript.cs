using UnityEngine;
using System.Collections;

public class HitBoxSizeScript : MonoBehaviour {

    public GameObject bumperFullLeft;
    public GameObject bumperFullRight;
    public GameObject bumperHalfLeft;
    public GameObject bumperHalfRight;

    private float screenWidth;
    public float height;
    public float fullStart;
    public float HalfStart;
    public float sizeMultiplyerFront;
    public float sizeMultiplyerBack;

    public float BumperRUpgrade = 0;
    public float BumperLUpgrade = 0;


	// Use this for initialization
	void Start () {
        UpdateSize();
	}
	
	// Update is called once per frame
	public void UpdateSize () {

        bumperFullLeft.transform.position = new Vector3((-(Screen.width * fullStart) * sizeMultiplyerBack + (BumperLUpgrade * .64F)), height, 0f);
        if ((-(Screen.width * fullStart) * sizeMultiplyerBack + BumperLUpgrade) > -4) bumperFullLeft.transform.position = new Vector3(-4f, height, 0f);

        bumperFullRight.transform.position = new Vector3(((Screen.width * fullStart) * sizeMultiplyerFront + (BumperRUpgrade * .64F)), height, 0f);
        if ((-(Screen.width * fullStart) * sizeMultiplyerFront + BumperRUpgrade) > -4) bumperFullRight.transform.position = new Vector3(-4f, height, 0f);

        bumperHalfLeft.transform.position = new Vector3((-(Screen.width * HalfStart) * sizeMultiplyerBack + (BumperLUpgrade)), height, 0f);
        if ((-(Screen.width * HalfStart) * sizeMultiplyerBack + (BumperLUpgrade * 1.5F)) > -4) bumperHalfLeft.transform.position = new Vector3(-4f, height, 0f);

        bumperHalfRight.transform.position = new Vector3(((Screen.width * HalfStart) * sizeMultiplyerFront + (BumperRUpgrade)), height, 0f);
        if ((-(Screen.width * HalfStart) * sizeMultiplyerFront + (BumperRUpgrade * 1.5F)) > -4) bumperHalfLeft.transform.position = new Vector3(-4f, height, 0f);

	}
}
