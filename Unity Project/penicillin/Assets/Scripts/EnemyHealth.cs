using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using GLOBAL;

public class EnemyHealth : MonoBehaviour, IDamage {

    public static event Action<int> Dead;
        
    public int maxHealth;
    public int currHealth;
    public int researchPoints = 10;//temporary value
    public GameObject DamageText;
    //GameObject player;
    Animator anim;
    bool isDead;
    //float stunDuration;
    //BoxCollider2D collider;
    Enemy enemy;
    EnemyManager manager;
    GameObject lvmanager;


    void Start () {
        lvmanager = GameObject.Find("Managers");
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
		ShowDamage (damage.ToString());
		if (currHealth <= 0) Death();
        //enemy.isStunned = true;	
    }

    void Death() {
		if (!isDead) {
			isDead = true;
			//anim.SetTrigger("isDead");
			manager.currEnemies--;
            lvmanager.GetComponent<StomachLevel_Global>().Addpoints(researchPoints);
            if(Dead != null) {
                Dead(1);
            }
            //player.GetComponent<StomachLevel_Global>().kills++;
			//GetComponent<Enemy>().enabled = false;
			Destroy(gameObject, 0.8f);
		}

    }

	void ShowDamage(string damage){
		GameObject temp = Instantiate (DamageText) as GameObject;
		RectTransform rt = temp.GetComponent<RectTransform> ();
		temp.transform.SetParent(transform.FindChild("EnemyCanvas"));
		rt.transform.localPosition = DamageText.transform.localPosition;
		rt.transform.localScale = DamageText.transform.localScale;
		rt.transform.localRotation = DamageText.transform.localRotation;
		temp.GetComponent<Text> ().text = damage;
		Destroy (temp, 1f);
	}
}
