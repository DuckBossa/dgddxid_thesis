﻿using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using GLOBAL;


public class PlayerHealth : MonoBehaviour {

    public int currHealth;
    public bool isInvulnerable;
    public float flashSpeed = 5f;
    public Image damageImage;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public Slider healthSlider;

    float currTime;
<<<<<<< HEAD
    Animator anim;
    PlayerMovement playerMovement;

    bool isDead;
    bool damaged;

    void Start() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
=======
    // Use this for initialization
    void Start () {
        currTime = 0;
>>>>>>> origin/master
        currHealth = GAME.max_health;
        isInvulnerable = false;
        healthSlider.value = currHealth;
    }

    void Update() {
        if (isInvulnerable) {
            currTime += Time.deltaTime;
            if (currTime > GAME.invulnerable_timer) {
                currTime = 0;
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
        damaged = true;
        if (!isInvulnerable) {
            currHealth--;
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
