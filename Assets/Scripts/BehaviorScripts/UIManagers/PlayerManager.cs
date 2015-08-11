using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    public int hp;
    public Text Health;
    public int ammo_std;
    public int ammo_BigSlow;
    public int ammo_emp;
    public Camera camera;

    public bool behavior1;
    public float behavior1Timer;
    public float behavior1Length;

    public bool behavior2;
    public float behavior2Timer;
    public float behavior2Length;

    public float BumperL = 0;
    public float BumperR = 0;

    public float[] Upgrades = new float[3]; 

    private float worldScreenHeight;
    private float worldScreenWidth;

    void Start()
    {
        behavior1 = false;
        behavior2 = false;
        worldScreenHeight = (float)Camera.main.orthographicSize;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
    }

    void Update()
    {
        if(behavior1)
        {
            if(behavior1Timer == null)
            {
                behavior1Timer = behavior1Length;
            }
            if(behavior1Timer <= 0)
            {
                DoBehavior1();
                behavior1Timer = behavior1Length;
            }
            if (behavior1Timer > 0)
            {
                behavior1Timer -= Time.deltaTime;
            }
        }
        if (behavior2)
        {
            if (behavior2Timer == null)
            {
                behavior2Timer = behavior2Length;
            }
            if (behavior2Timer <= 0)
            {
                DoBehavior2();
                behavior2Timer = behavior2Length;
            }
            if (behavior2Timer > 0)
            {
                behavior2Timer -= Time.deltaTime;
            }
        }
    }

    public void TakeDamage(int ammount) {
        var hitBox = GetComponent<HitBoxSizeScript>();
        hp -= ammount;
        Health.GetComponent<Health>().UpdateHealth(hp);
        if (behavior1) behavior1 = false;
        if (behavior2) behavior2 = false;
        BumperL = 0;
        BumperR = 0;
        hitBox.BumperRUpgrade = BumperR;
        hitBox.BumperLUpgrade = BumperL;
        if(hp <= 0)
        {
            GetComponent<GameManager>().GameOver();
        }
    }

    public void DoBehavior1()
    {
        Vector3 startPos = this.GetComponent<GameManager>().PlayerMissileSpawnLocation;
        var missileAway = Instantiate(this.GetComponent<GameManager>().Missile, startPos, Quaternion.identity) as GameObject;
        var tx = getNewX();
        while(tx <= 5 && tx >= -5)
        {
            tx = getNewX();
        }
        float ty = (Random.Range((-2f * worldScreenHeight / 6), (-1.5f * worldScreenHeight / 6)));
        var t = new Vector3(tx, ty, 0);
        missileAway.GetComponent<MissileScript>().target = t;
        missileAway.GetComponent<MissileScript>().type = "Flak";
    }
    
    private float getNewX()
    {
        float a = Random.Range(worldScreenWidth, -worldScreenWidth); 
        return a;
    }

    public void DoBehavior2()
    {
        Vector3 startPos = this.GetComponent<GameManager>().PlayerMissileSpawnLocation;
        var missileAway = Instantiate(this.GetComponent<GameManager>().Laser, startPos, Quaternion.identity) as GameObject;
        Debug.Log("FIRING MA LAZER"); 
    }

    public void getAmmo(string type, int amount)
    {

        var hitBox = GetComponent<HitBoxSizeScript>();

        switch (type)
        {
            case "Standard":
                ammo_std += amount;
                break;
            case "BigSlow":
                ammo_BigSlow += amount;
                break;
            case "EMP":
                ammo_emp += amount;
                break;
            case "Blast Radius":
                Upgrades[2]++;
                break;
            case "Children Missiles":
                Upgrades[1]++;
                break;
            case "hard Shell":
                Upgrades[0] = 1;
                break;
            case "Blast Shield":
                int coin = Random.Range(0, 1);
                if (coin > 0)
                {
                    BumperL += amount;
                    hitBox.BumperLUpgrade = BumperL;
                }
                else
                {
                    BumperR -= amount;
                    hitBox.BumperRUpgrade = BumperR;
                }
                hitBox.UpdateSize();
                break;
            default:
                ammo_std += amount;
                break;
        }
    }
}
