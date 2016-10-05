using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using GLOBAL;


public class PlayerHealth : MonoBehaviour {

    public int currHealth;
    public static bool isInvulnerable;
    public float flashSpeed = 5f;
    public Image damageImage, fill;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Slider healthSlider;

    float currTime;
    Animator anim;
    PlayerMovement playerMovement;

    bool isDead;
    bool damaged;
    Color deadColor, aliveColor;


    void Start() {
        currTime = 0;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        currHealth = GAME.max_health;
        healthSlider.maxValue = GAME.max_health;
        isInvulnerable = false;
        healthSlider.value = currHealth;
        currTime = 0;
        deadColor = new Color(1f, 0f, 0f, 1f);
        aliveColor = new Color(0f, 1f, 0f, 1f);
        fill.color = aliveColor;
    }

    void Update() {
        if (isInvulnerable) {
            currTime += Time.deltaTime;
            if (currTime > GAME.invulnerable_timer) {
                isInvulnerable = false;
            }
        }

        if (damaged) {
            damageImage.color = flashColor;
        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    public void TakeDamage() {
        if (!isInvulnerable) {
            damaged = true;
            fill.color = Color.Lerp(deadColor, aliveColor, currHealth/GAME.max_health);
            currHealth--;
            currTime = 0;
            healthSlider.value = currHealth;
            if (currHealth <= 0) {
                Death();
            }
            isInvulnerable = true;
        }
    }

    public void GainHealth() {
        if (currHealth < GAME.max_health) {
            currHealth++;
        }
    }

    void Death() {
        isDead = true;
        //anim.SetTrigger("Die");
        playerMovement.enabled = false;
    }
}
