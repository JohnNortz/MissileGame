using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveManager : MonoBehaviour {

    public int wave;
    public int difficulty;   //start 1 to 5 hardest
    private int barrage;
    private int volley;
    private float volleyTimer;
    public float waveGap;
    public float barrageGap;
    public float volleyGap;
    public int barrageMax;
    public int volleyMax;
    public int volleyAmmount;
    public float volleyMinus;
    public Text UI_Wave;
    private string missileType;
    public GameObject startPanel;
    public bool Going = false;
    private float upgradeTime;
    public int upgradeFrequency;

    public GameObject defaultMissile;
    public GameObject OrbitalObject;

    private float[] currentVolley;

    private float worldScreenHeight; 
    private float worldScreenWidth;

	// Use this for initialization
	void Start () {

        worldScreenHeight = (float)Camera.main.orthographicSize;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


	}
	
	// Update is called once per frame
	void Update () {
        if (Going)
        {
            if (volleyTimer > 0) volleyTimer -= Time.deltaTime;

            if (volleyTimer <= 0)
            {
                if (volley > volleyMax)
                {
                    LaunchNewBarrage();
                }
                else
                {
                    LaunchNewVolley();
                }
            }

            if (currentVolley != null)
            {
                for (int i = 0; i < currentVolley.Length; i++)
                {
                    if (volleyMinus >= currentVolley[i] && currentVolley[i] != -1f)
                    {
                        if (wave > 13 || i % 3 == 0)
                        {
                            int temp = Random.Range(0, 6);
                            switch (temp)
                            {
                                case 0:
                                    missileType = "default";
                                    break;
                                case 1:
                                    missileType = "Sidewinder";
                                    break;
                                case 2:
                                    missileType = "BigExplosion";
                                    break;
                                case 3:
                                    missileType = "ClusterRockets";
                                    break;
                                case 4:
                                    missileType = "Fast";
                                    break;
                                case 5:
                                    missileType = "Rock";
                                    break;
                            }
                        }
                        LaunchMissile(missileType);
                        if (missileType != "Rock") currentVolley[i] = -1f;
                        else currentVolley[i] = currentVolley[i] + .2f;
                    }
                }
                volleyMinus += Time.deltaTime;
                upgradeTime -=Time.deltaTime;

                if (upgradeTime <= 0)
                {
                    int Rolling = Random.Range(1, 10);
                    if (Rolling == 2 || Rolling == 3 || Rolling == 7 || Rolling == 8) LaunchSupply("Upgrade");
                    if (Rolling == 5 || Rolling == 6) LaunchSupply("Upgrade");
                    LaunchSupply("Upgrade");
                    if (wave > 6) LaunchSupply("Upgrade");

                    upgradeTime = upgradeFrequency;
                }
            }
        }
	}

    void DifficultyUp() {
        if (difficulty == 0)
        {
            difficulty = 1;
            barrageMax = 2;
            volleyMax = 3;
            volleyAmmount = 3;
        }
        else
        {
            if (barrageMax < 3)barrageMax++;
            if (volleyMax < 4)volleyMax++;
            volleyAmmount++;                      //so WAVE15 has    10 barrages of 5 volleys of 15 missiles       750 missiles
        }
    }

    void LaunchNewWave()
    {
        wave = wave + 1;   //THIS IS WHERE WAVE NUMBER + 1 IS CALLED !!
        barrage = 0;
        volley = 0;
        volleyTimer = waveGap;
        UI_Wave.text = wave.ToString();
        if (wave % 2 == 0) DifficultyUp();
        switch(wave)
        {
            case 1:
                missileType = "default";
                break;
            case 2:
                missileType = "Sidewinder";
                break;
            case 3:
                missileType = "default";
                break;
            case 4:
                missileType = "Sidewinder";
                break;
            case 5:
                missileType = "ClusterRockets";
                break;
            case 6:
                missileType = "Sidewinder";
                break;
            case 7:
                missileType = "BigExplosion";
                break;
            case 8:
                missileType = "ClusterRockets";
                break;
            case 9:
                missileType = "ClusterRockets";
                break;
            case 10:
                missileType = "Sidewinder";
                break;
            case 11:
                missileType = "BigExplosion";
                break;
            case 12:
                missileType = "ClusterRockets";
                break;
            case 13:
                missileType = "ClusterRockets";
                break;
            default:
                missileType = "default";
                break;
        }
           
    }

    void LaunchNewBarrage()
    {
        if (barrage == barrageMax)
        {
            LaunchNewWave();
        }
        else
        {
            volley = 0;
            volleyTimer = barrageGap;
            barrage++;
        }

    }

    void LaunchNewVolley()
    {
        currentVolley = new float[volleyAmmount];      //creates an array of size VolleyAmmount with delay times inserted in the for loop;
        for (int i = 0; i < volleyAmmount; i++)
        {
            currentVolley[i] = Random.Range(3f, 0f);   //launches a missile and removes this value as VolleyMinus (timer) exceeds its value;
        }
        Debug.Log("volley: " + volley + " (" + volleyAmmount + "missiles launched) " + " || barrage: " + barrage + " || wave: " + wave);
        volleyTimer = volleyGap;
        volleyMinus = 0;
        volley++;
    }

    void LaunchMissile(string type)
    {
        Vector3 startPos = new Vector3(Random.Range(worldScreenWidth, -worldScreenWidth), worldScreenHeight, 0);
        var missileAway = Instantiate(defaultMissile, startPos, Quaternion.identity) as GameObject;
        missileAway.GetComponent<MissileScript>().target = new Vector3(Random.Range(worldScreenWidth, -worldScreenWidth), -worldScreenHeight, 0);
        missileAway.GetComponent<MissileScript>().type = type;
    }

    void LaunchSupply(string type)
    {
        Vector3 pos = transform.position;
        var missileAway = Instantiate(OrbitalObject, pos, Quaternion.identity) as GameObject;
        missileAway.GetComponent<orbitalObject>().type = type;
    }

    public void StartGame()
    {
        wave = 0;
        barrage = 0;
        volley = 0;
        difficulty = 0;
        Going = true;
        if(startPanel != null) Destroy(startPanel.gameObject);

        DifficultyUp();
        LaunchNewWave();
    }
}
