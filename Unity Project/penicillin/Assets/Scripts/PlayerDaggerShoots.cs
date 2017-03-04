using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GLOBAL;

public class PlayerDaggerShoots : MonoBehaviour {
    public List<GameObject> projs = new List<GameObject>();
    public Transform spawn_point;
    PlayerMovement pm;
	PlayerAttack pa;
    ResitanceCalculator rc;
    float timer = 0f;

    public void Awake() {
        rc = GetComponent<ResitanceCalculator>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAttack>();
    }

    public void Start() {
    }

    public void Update() {
        if(timer < GAME.playerdagger_aspd) {
            timer += Time.deltaTime;
        }
    }    

    public void fire() {
        if (timer >= GAME.playerdagger_aspd) {
            timer = 0f;
			var tempRot = projs[pa.GetWeapLevel (1)].transform.rotation.eulerAngles;
			var temp = Instantiate(projs[pa.GetWeapLevel (1)], transform.position, Quaternion.Euler(tempRot.x,tempRot.y + pm.getDir() > 0 ? 0f : 180f,tempRot.z)) as GameObject;
            temp.GetComponent<MoveDir>().setDir(pm.getDir() > 0 ? Vector2.left * GAME.playerdagger_speed : Vector2.right * GAME.playerdagger_speed);
			temp.GetComponentInChildren<TakeDamage> ().SetDamage ( (int) (GAME.WEAP_DAMAGE [1, pa.GetWeapLevel (1)] * (1 /*can be modified to be above???*/ - rc.GetResistanceModifier(1))) );

        }
    }
    
}
