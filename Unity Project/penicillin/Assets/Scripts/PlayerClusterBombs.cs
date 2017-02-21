using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using GLOBAL;

public class PlayerClusterBombs : MonoBehaviour {

	public List<GameObject> projs = new List<GameObject>();
	PlayerMovement pm;
	PlayerAttack pa;
	int switcheroonie;
	float timer = 0f;


	public void Start(){
		pm = GetComponent<PlayerMovement> ();
		pa = GetComponent<PlayerAttack> ();

	}

	public void Update(){
		if(timer < GAME.playerclusterbomb_aspd) {
			timer += Time.deltaTime;
		}
	}

	public void fire(){
		if (timer >= GAME.playerclusterbomb_aspd) {
			timer = 0f;
			var temp = Instantiate(projs[switcheroonie], transform.position, projs[switcheroonie].transform.rotation) as GameObject;
			switcheroonie = (switcheroonie + 1) % 2  + pa.GetWeapLevel(2) * 2;
			temp.GetComponent<MoveDir>().setDir(pm.getDir() > 0 ? Vector2.left * GAME.playerclusterbomb_speed : Vector2.right * GAME.playerclusterbomb_speed);
		}	
	}

}
