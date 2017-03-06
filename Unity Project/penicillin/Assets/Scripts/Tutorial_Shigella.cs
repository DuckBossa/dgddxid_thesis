using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Tutorial_Shigella : MonoBehaviour, IDamage {

    public int maxHealth;
    public int currHealth;
    public int researchPoints = 10;//temporary value
    public GameObject DamageText, score;

    //private ScoreManager smgr;


    //GameObject player;
    Animator anim;
    bool isDead;
    //float stunDuration;
    //BoxCollider2D collider;
    Enemy enemy;

	void Start () {
        //smgr = score.GetComponent<ScoreManager>();
        //collider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
        //player = GameObject.Find("Penny");
        //stunDuration = .5f;
		TakeDamage(1);
        enemy = GetComponent<Enemy>();
    }

	public void TakeDamage(int damage) {
        if (isDead) return;
		currHealth -= damage;
		ShowDamage (damage.ToString());
		if (currHealth <= 0) Death();
        enemy.isStunned = true;	
    }

    void Death() {
		if (!isDead) {
			isDead = true;
			//anim.SetTrigger("isDead");
			//smgr.researchPoints += researchPoints;
			//smgr.totalResearchPoints += researchPoints;
            this.gameObject.SetActive(false);
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
