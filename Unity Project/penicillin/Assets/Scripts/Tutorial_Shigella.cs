using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Tutorial_Shigella : MonoBehaviour, IDamage {

    public int maxHealth;
    public int currHealth;
    public int researchPoints = 10;//temporary value
    public GameObject DamageText, score, mgr;

    Animator anim;
    bool isDead;
    //float stunDuration;
    //BoxCollider2D collider;

	void Start () {
        anim = GetComponent<Animator>();
        currHealth = maxHealth;
    }

	public void TakeDamage(int damage) {
        if (isDead) return;
		currHealth -= damage;
		ShowDamage (damage.ToString());
		if (currHealth <= 0) Death();
    }

    void Death() {
		if (!isDead) {
			isDead = true;
            this.gameObject.SetActive(false);
            mgr.GetComponent<Tutorial>().enemySpawned = true;
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
