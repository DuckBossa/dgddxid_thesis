using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GLOBAL;

public class PlayerDaggerShoots : MonoBehaviour {
    public List<GameObject> projs = new List<GameObject>();
    PlayerMovement pm;
    int switcheroonie;
    float timer = 0f;

    public void Start() {
        switcheroonie = 0;
        pm = GetComponent<PlayerMovement>();
    }

    public void Update() {
        if(timer < GAME.playerdagger_aspd) {
            timer += Time.deltaTime;
        }
    }    

    public void fire() {
        if (timer >= GAME.playerdagger_aspd) {
            timer = 0f;
            var tempRot = projs[switcheroonie].transform.rotation.eulerAngles;
            var temp = Instantiate(projs[switcheroonie], transform.position, Quaternion.Euler(tempRot.x,tempRot.y + pm.getDir() > 0 ? 0f : 180f,tempRot.z)) as GameObject;
            switcheroonie = (switcheroonie + 1) % 2;
            temp.GetComponent<MoveDir>().setDir(pm.getDir() > 0 ? Vector2.left * GAME.playerdagger_speed : Vector2.right * GAME.playerdagger_speed);
        }
    }

}
