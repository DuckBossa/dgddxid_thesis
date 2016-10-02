using UnityEngine;
using System.Collections;
using System.IO;
using GLOBAL;


public class PlayerHealth : MonoBehaviour {

    public int currHealth;
    public bool isInvulnerable;
    float currTime;
    // Use this for initialization
    void Start () {
        currHealth = GAME.max_health;
        isInvulnerable = false;
    }
    
    void Update() {
        if (isInvulnerable) {
            currTime += Time.deltaTime;
            if (currTime > GAME.invulnerable_timer) {
                isInvulnerable = false;
            }
        }
    }

    public void TakeDamage() {
        if (!isInvulnerable) {
            currHealth--;
            if (currHealth <= 0) {
                Debug.Log("Call the you lose event");
                //Application.LoadLevel("End");
            }
            isInvulnerable = true;
        }
    }

    public void GainHealth() {
        if(currHealth < GAME.max_health) {
            currHealth++;
        }
    }
}
