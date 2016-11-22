using UnityEngine;
using System.Collections;
using GLOBAL;

public class AcidDotPlayer : MonoBehaviour {
    public PlayerHealth ph;
    float currTime;

    // Use this for initialization
    void Start() {
        currTime = 0;
    }


    void Update() {
        if(currTime < GAME.acid_dot_timer) {
            currTime += Time.deltaTime;
        }
    }


    void OnTriggerEnter2D(Collider2D other){
        //player take damage;
        if(currTime > GAME.acid_dot_timer) {
            ph.TakeDamage();
            currTime = 0;
        }  
    }

    void OnTriggerStay2D(Collider2D other) {
        currTime += Time.deltaTime;
        if (currTime > GAME.acid_dot_timer) {
            ph.TakeDamage();
            currTime = 0;
        }
    }
}
