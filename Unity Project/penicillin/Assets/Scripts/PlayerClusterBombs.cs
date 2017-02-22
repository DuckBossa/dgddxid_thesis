using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using GLOBAL;

public class PlayerClusterBombs : MonoBehaviour {

	public List<GameObject> projs = new List<GameObject>();
    public GameObject spawn_point;
	PlayerMovement pm;
	PlayerAttack pa;
	int switcheroonie;
	float timer = 0f;
    int currAmmo;

    public void Awake() {
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAttack>();
    }

    public void Start(){
        ReplenishAmmo();
	}

	public void Update(){
		if(timer < GAME.playerclusterbomb_aspd) {
			timer += Time.deltaTime;
		}
	}

	public void bomb(){
		if (timer >= GAME.playerclusterbomb_aspd && currAmmo > 0) {
			timer = 0f;
            currAmmo--;
			var temp = Instantiate(projs[switcheroonie], spawn_point.transform.position, projs[switcheroonie].transform.rotation) as GameObject;
			//switcheroonie = (switcheroonie + 1) % 2  + pa.GetWeapLevel(2) * 2;
			temp.GetComponent<MoveDir>().setDir(pm.getDir() > 0 ? Vector2.left * GAME.playerclusterbomb_speed : Vector2.right * GAME.playerclusterbomb_speed);
			temp.GetComponentInChildren<ClusterBomb>().SetDamage(GAME.WEAP_DAMAGE[2, pa.GetWeapLevel(2)]);
        }	
	}

    public void ReplenishAmmo() {
        currAmmo = GAME.WEAP_DURABILITY[2, pa.GetWeapLevel(2)];
    }

}
