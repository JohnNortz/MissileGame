using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Distance : MonoBehaviour {

    public Camera GameManager;
    public Text _text;
    public float _Distance;

	// Update is called once per frame
	void Update () {
	    _Distance = GameManager.GetComponent<GameManager>().Distance;
        _text.text = _Distance.ToString("[ 00.00 ]");
	}
}
