using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerDaggerShoots : MonoBehaviour {
    public List<GameObject> projs = new List<GameObject>();
    int switcheroonie;
    public float firerate = 0.4f;
    float timer = 0f;

    public void Start() {
        switcheroonie = 0;
    }

    public void Update() {
        if(timer < firerate) {
            timer += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space)) {
            if(timer >= firerate) {
                timer = 0f;
                var temp = Instantiate(projs[switcheroonie],transform.position,projs[switcheroonie].transform.rotation) as GameObject;
                switcheroonie = (switcheroonie + 1) % projs.Count;
                temp.GetComponent<MoveDir>().setDir(Vector2.left);
            }
           
        }
    }    

}
