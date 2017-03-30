using UnityEngine;
using System.Collections;
using System.IO;
using System;
using UnityEngine.UI;
using GLOBAL;


public class PlayerHealth : MonoBehaviour {

    public static event Action Dead;

    public GameObject mgr;
    public int currHealth;
    public static bool isInvulnerable;
    public float flashSpeed = .5f;
    public Image damageImage, fill;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Slider healthSlider;
    public Canvas hud, pause, loadout, gameover,gamewon, controls;

    float currTime;
    Animator anim;
    PlayerMovement playerMovement;

    bool isDead;
    bool damaged;
    bool isWon;
    Color deadColor, aliveColor;
    

    void Start() {
        currTime = 0;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
		anim.updateMode = AnimatorUpdateMode.Normal;
        currHealth = GAME.max_health;
        healthSlider.maxValue = GAME.max_health;
        healthSlider.value = currHealth;
        isInvulnerable = false;
        currTime = 0;
        isDead = false;
        isWon = false;
        anim.SetBool("gameOver", isDead);
        deadColor = new Color(1f, 0f, 0f, 1f);
        aliveColor = new Color(0f, 1f, 0f, 1f);
        fill.color = aliveColor;
    }

    void OnEnable() {
        ShigellangController.Dead += Success;
    }

    void OnDisable() {
        ShigellangController.Dead -= Success;
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
        if (!isInvulnerable && !playerMovement.amDashing() && !isDead) {
            anim.SetTrigger("isOuchie");
            damaged = true;
            fill.color = Color.Lerp(deadColor, aliveColor, (float) currHealth/GAME.max_health);
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
        healthSlider.value = currHealth < GAME.max_health ? currHealth += GAME.healthpickupvalue : currHealth;
    }


    public void NotOuchie(){
		anim.SetBool ("isOuchie", false);
	}
    void Death() {
        isDead = true;
        anim.SetBool("gameOver", isDead);
        anim.SetTrigger("die");
        if( Dead!= null) {
            Dead();
        }
        /// Death animation?
        //Debug.Log("player dead");
        //anim.SetTrigger("Die");
        //playerMovement.enabled = false;
        //GameOverScreenHandler.displayStats();
        
    }

    public bool IsOver() {
        return isDead || isWon;
    }

    void PostGameDeath() {
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        StartCoroutine("ShowStatsDeath");
    }

    IEnumerator ShowStatsDeath() {
        yield return new WaitForSeconds(1.0f);
        mgr.GetComponent<StomachLevel_Global>().PennyDied();
    }

	public void Success() {
		anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        anim.SetBool("gameOver", true);
        anim.SetTrigger("win");
    }

    public void Win() {
        if (!isWon) {
            isWon = true;

            StartCoroutine("Won");
        }
    }


    IEnumerator Won() {
        yield return new WaitForSeconds(1.0f);
        mgr.GetComponent<StomachLevel_Global>().PennyWon();
    }

    IEnumerator ShowStatsWon() {
        yield return new WaitForSeconds(1.0f);
    }

}
