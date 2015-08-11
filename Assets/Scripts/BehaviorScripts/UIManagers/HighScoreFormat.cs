using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreFormat : MonoBehaviour {

    public Text TheScores;
    public string[] names = new string[10];
    public int[] scores = new int[10];
    public float[] distances = new float[10];

    public GameObject deathMark;
    public GameObject PlayerSprite;
    public Vector3 deathLanePos;

    void Start()
    {
        names = new string[10];
        scores = new int[10];
        distances = new float[10];

    }

	// Update is called once per frame
	public void UpdateScores () {
        string _distance = "";
        for (int i = 0; i < 5; i++)
        {
            if (i < 5)
            {
                _distance = _distance + names[i] + ":  " + distances[i].ToString() + "\n";
            }
        }
        TheScores.text = _distance;
	}
    public void GetDeadPlayers()
    {
        string _distance = "";
        for (int i = 0; i < 5; i++)
        {
            if (i < 5)
            {
                _distance = _distance + names[i] + ":  " + distances[i].ToString() + "\n";
            }
        }
        TheScores.text = _distance;
        int current = 0;
        foreach (var name in names)
        {
            float currentScore = distances[current];
            var pos = deathLanePos;
            pos.x = PlayerSprite.transform.position.x + currentScore;
            var deathMarker = Instantiate(deathMark, pos, Quaternion.identity) as GameObject;
            deathMarker.GetComponent<DeadSpriteMotion>().playerName = names[current];
            current++;
        }
    }
}
