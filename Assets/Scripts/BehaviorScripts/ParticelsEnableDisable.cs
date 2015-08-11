using UnityEngine;
using System.Collections;

public class ParticelsEnableDisable : MonoBehaviour {

    public void Enable()
    {
        gameObject.GetComponent<ParticleSystem>().enableEmission = true;
    }
    public void Disable()
    {
        gameObject.GetComponent<ParticleSystem>().enableEmission = false;
    }
}
