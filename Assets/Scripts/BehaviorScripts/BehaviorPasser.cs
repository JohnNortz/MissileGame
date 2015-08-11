using UnityEngine;
using System.Collections;

public class BehaviorPasser : MonoBehaviour {

    public GameObject _parent;
    public ParticleSystem part1;
    public ParticleSystem part2;

	// Use this for initialization
    void Start()
    {
        part2.enableEmission = false;
	
	}
	
	// Update is called once per frame
	void PassBehavior() {
        var go = _parent.GetComponent<LandedRocketScript>();
        go.Launch();
	}

    void part1Disable()
    {
        part1.enableEmission = false;
    }

    void part2Enable()
    {
        part2.enableEmission = true;
    }
}
