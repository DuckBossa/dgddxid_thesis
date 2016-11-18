using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Shigellang_Dormant : MonoBehaviour {

    public Slider healthSlider;
    public Sprite current, dmg1, dmg2, broken;
    public int health;
    private float damageTimer;
    private bool vulnerable;
    private Animator myAnim;

	void Start () {
        myAnim = GetComponent<Animator>();
        healthSlider.maxValue = GAME.Shigellang_Dormant_MaxHealth;
        healthSlider.value = healthSlider.maxValue;
        healthSlider.minValue = 0;
        health = GAME.Shigellang_Dormant_MaxHealth;
        vulnerable = true;
    }
	
    void OnEnable() {
        healthSlider.gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collision2D col) {
        TakeDamage();
    }

	void Update () {
        damageTimer += Time.deltaTime;
        if(damageTimer > GAME.Shigellang_Dormant_TimeBetweenAttacks) {
            vulnerable = true;
        }
        //2/3 health
        if (health > GAME.Shigellang_Dormant_MaxHealth / 3 && health < 2 * GAME.Shigellang_Dormant_MaxHealth / 3) {
            GetComponent<SpriteRenderer>().sprite = dmg1;
        }
        //1/3 health
        else if (health > 1 && health < GAME.Shigellang_Dormant_MaxHealth / 3) {
            GetComponent<SpriteRenderer>().sprite = dmg2;
        }
        //broken
        else if (health == 1) { //1 more hit to finally break 
            GetComponent<SpriteRenderer>().sprite = broken;
        }
        //ded
        else if(health == 0) {
            //play awakening animation

            //spawn the boss

            //set gameobject to inactive
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage() {
        if(vulnerable) {
            myAnim.SetTrigger("takeDamage");
            healthSlider.value = --health;
            vulnerable = false;
            damageTimer = 0;
        }
    }
}
