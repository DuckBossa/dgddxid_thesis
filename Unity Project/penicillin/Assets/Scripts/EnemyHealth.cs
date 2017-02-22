using UnityEngine;
using System.Collections;
using GLOBAL;

public class EnemyHealth : MonoBehaviour, IDamage {

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

	public void TakeDamage(int damage) {
        if (isDead) return;
		currHealth -= damage;
        if (currHealth <= 0) Death();
        enemy.isStunned = true;	
    }

    void Death() {
		if (!isDead) {
			isDead = true;
			//anim.SetTrigger("isDead");
			manager.currEnemies--;
			ScoreManager.researchPoints += researchPoints;
			ScoreManager.totalResearchPoints += researchPoints;
			Debug.Log (ScoreManager.researchPoints + " points now jacky mao");
			//GetComponent<Enemy>().enabled = false;
			Destroy(gameObject, 0.3f);
		}

    }
}
