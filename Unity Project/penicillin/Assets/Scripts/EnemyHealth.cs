using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currHealth;
    public int researchPoints = 10;//temporary value

    //GameObject player;
    Animator anim;
    bool isDead;
    //float stunDuration;
    //BoxCollider2D collider;
    Enemy enemy;
    EnemyManager manager;

	void Start () {
        //collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        //player = GameObject.Find("Penny");
        manager = GameObject.Find("SpawnControllers").GetComponent<EnemyManager>();
        //stunDuration = .5f;
        enemy = GetComponent<Enemy>();
    }

    public void TakeDamage() {
        if (isDead) return;
        currHealth --;
        if (currHealth <= 0) Death();
        enemy.isStunned = true;
    }

    void Death() {
        isDead = true;
        anim.SetBool("isDead", true);
        manager.currEnemies--;
        Destroy(gameObject, .75f);
        ScoreManager.researchPoints += researchPoints;
        ScoreManager.totalResearchPoints += researchPoints;
        GetComponent<Enemy>().enabled = false;
        // collider.isTrigger = true; //they don't collide so this isn't necessary
    }
}
