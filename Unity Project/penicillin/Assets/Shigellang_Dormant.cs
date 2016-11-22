using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Shigellang_Dormant : MonoBehaviour, IDamage {

    public Slider healthSlider;
    public Sprite current, dmg1, dmg2, broken, awake;
    public Image bossIcon;
    public GameObject Fighting_Shigella;
    public GameObject BossProtectors;
    public TileDeconstructionConstruction tdc;
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
        BossProtectors.GetComponent<EnemyManager>().enabled = true;
    }
	
    void OnEnable() {
        healthSlider.gameObject.SetActive(true);
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
            //reconfigure slider
            bossIcon.sprite = awake;
            healthSlider.maxValue = GAME.Shigellang_Fighting_MaxHealth;
            healthSlider.value = healthSlider.maxValue;
            healthSlider.minValue = 0;
            //spawn the boss
            tdc.BossBattle();
            GameObject temp = Instantiate(Fighting_Shigella, transform.position, Quaternion.identity) as GameObject;
            temp.GetComponent<ShigellangController>().healthSlider = healthSlider;
            //set gameobject to inactive
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int dmg) {
        if(vulnerable) {
            myAnim.SetTrigger("takeDamage");
            healthSlider.value = --health;
            vulnerable = false;
            damageTimer = 0;
        }
    }
}
