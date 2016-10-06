using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currHealth;
    public int researchPoints = 10;//temporary value
    public float sinkSpeed = 2.5f;
    GameObject player;

    Animator anim;
    bool isDead;
    BoxCollider2D collider;

	void Start () {
        collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        player = GameObject.Find("Penny");
    }

    public void TakeDamage() {
        if (isDead) return;
        currHealth --;
        if (currHealth <= 0) Death();
    }

    void Death() {
        isDead = true;
        anim.SetBool("isDead", true);
        Destroy(gameObject, .75f);
        ScoreManager.researchPoints += researchPoints;
        GetComponent<Enemy>().enabled = false;
        // collider.isTrigger = true; //they don't collide so this isn't necessary
    }
}
