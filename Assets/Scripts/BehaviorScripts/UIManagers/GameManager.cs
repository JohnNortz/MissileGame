using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int hp;
    public float Score;
    public float Distance;
    public GameObject Missile;
    public GameObject Laser;

    private GameObject MissileToShoot;
    public GameObject PlayerMissileSpawnLocationObj;
    public Vector3 PlayerMissileSpawnLocation;
    private GameObject lastMissile;

    public RawImage deathMenu;

	// Use this for initialization
	public void Go () {
        Distance = 0;
        MissileToShoot = Missile;
        PlayerMissileSpawnLocation = PlayerMissileSpawnLocationObj.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 ScreenLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shoot(ScreenLoc);
        }
        
        Distance += (Time.deltaTime * 1.4f);
    }

    void Shoot(Vector3 target) {
        var loc = GetComponent<PlayerManager>();
        if(loc.ammo_BigSlow == 0 && loc.ammo_emp == 0 && loc.ammo_std > 0)
        {
            var Missile = Instantiate(MissileToShoot, PlayerMissileSpawnLocation, Quaternion.identity) as GameObject;
            var giveMissile = Missile.GetComponent<MissileScript>();
            giveMissile.target = target;
            GetComponent<PlayerManager>().ammo_std--;
        }
        else if(loc.ammo_BigSlow > 0)
        {
            var Missile = Instantiate(MissileToShoot, PlayerMissileSpawnLocation, Quaternion.identity) as GameObject;
            var giveMissile = Missile.GetComponent<MissileScript>();
            giveMissile.target = target;
            giveMissile.type = "BigExplosion";
            GetComponent<PlayerManager>().ammo_BigSlow--;
        }
        else if(loc.ammo_emp > 0)
        {
            var Missile = Instantiate(MissileToShoot, PlayerMissileSpawnLocation, Quaternion.identity) as GameObject;
            var giveMissile = Missile.GetComponent<MissileScript>();
            giveMissile.target = target;
            GetComponent<PlayerManager>().ammo_emp--;
        }
    }

    public void TakeDamage() {

    }

    public void GameOver() {
        deathMenu.GetComponent<EnableDisableAllChildren>().flop();
    }
}
