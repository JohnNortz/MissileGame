using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public float _Score;
    public Text ScoreText;
    
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        ScoreText.text = _Score.ToString();
	}

    public void AddScore(float toAdd)
    {
        _Score = _Score + toAdd;
    }
}
