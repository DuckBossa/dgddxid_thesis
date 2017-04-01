using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Shigellang_Dormant : MonoBehaviour, IDamage {

    public Slider healthSlider;
    public Sprite current, dmg1, dmg2, broken, awake;
    public Image bossIcon;
    public GameObject Fighting_Shigella;
    public EnemyManager em;
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
        em.enabled = true;
    }
	
    void OnEnable() {
        healthSlider.gameObject.SetActive(true);
    }

	void Update () {
        damageTimer += Time.deltaTime;
        if(damageTimer > GAME.Shigellang_Dormant_TimeBetweenAttacks) {
            vulnerable = true;
        }
       
    }

    public void TakeDamage(int dmg) {
        if(vulnerable) {
            myAnim.SetTrigger("takeDamage");
            health -= dmg;
            if(health < 0) {
                healthSlider.value = 0;
            }
            else {
                healthSlider.value = health;
            }
            vulnerable = false;
            damageTimer = 0;

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
            else if (health <= 0) {
                //play awakening animation
                //reconfigure slider
                bossIcon.rectTransform.sizeDelta = new Vector2(awake.rect.width/2, awake.rect.height/2);
                bossIcon.sprite = awake;
                bossIcon.transform.position = new Vector3(bossIcon.transform.position.x, bossIcon.transform.position.y - awake.rect.height / 10, bossIcon.transform.position.z);
                healthSlider.maxValue = GAME.Shigellang_Fighting_MaxHealth;
                healthSlider.value = healthSlider.maxValue;
                healthSlider.minValue = 0;
                //spawn the boss
                Fighting_Shigella.SetActive(true);
                Fighting_Shigella.GetComponent<ShigellangController>().healthSlider = healthSlider;
                //set gameobject to inactive
                gameObject.SetActive(false);
            }

        }
    }
}
