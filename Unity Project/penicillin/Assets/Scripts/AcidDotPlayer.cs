using UnityEngine;
using System.Collections;
using GLOBAL;

public class AcidDotPlayer : MonoBehaviour {
    public PlayerHealth ph;
    float currTime;

	// Use this for initialization
	void Start () {
        currTime = 0;
	}
	
	// Update is called once per frame
    
    void OnTriggerEnter2D(Collider2D other){
        //player take damage;
        Debug.Log("d");
        ph.TakeDamage();
        currTime = 0;
    }

    void OnTriggerStay2D(Collider2D other) {
        currTime += Time.deltaTime;
        if (currTime > GAME.acid_dot_timer) {
            Debug.Log("c");
            ph.TakeDamage();
            currTime = 0;
        }
        //timer start 
    }

    void OnTriggerExit2D(Collider2D other) {
      //exit la
    }
}
