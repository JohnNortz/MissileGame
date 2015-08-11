using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ammo : MonoBehaviour {

    public Camera GameManager;
    public Text _text;
    public float _Ammo;

	// Update is called once per frame
    void Update()
    {
        _Ammo = GameManager.GetComponent<PlayerManager>().ammo_std;
        _text.text = _Ammo.ToString("[ 0 ]");
    }
}
