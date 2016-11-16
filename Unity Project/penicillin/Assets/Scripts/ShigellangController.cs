using UnityEngine;
using System.Collections;
using GLOBAL;


public class ShigellangController : MonoBehaviour {
    public LayerMask mask;

    float leapTimer;
    float leapAttackTimer;
    float projectileSpewTimer;
    float idleTimer;
    float distTraveled;
    float idleTimerMax;
    Rigidbody2D rb;
    Animator anim;
    Vector2 dirWalk;
    Vector2 dirLeap;
    bool isIdle;
    bool isWalking;
    bool isLeaping;
    bool playerSpotted;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leapTimer = leapAttackTimer = projectileSpewTimer = idleTimer = 0;
        dirWalk = Vector2.left;
        isWalking = false;
        isLeaping = false;
        isIdle = true;
        idleTimerMax = Random.Range(0.5f, GAME.Shigellang_TimeIdleRange);
	}

    void setVel(Vector2 vel) {
        rb.velocity = vel;
    }



    void Update() {
        anim.SetFloat("Direction", dirWalk.x > 0 ? 1f : 0f);
        anim.SetBool("isWalking", isWalking);
    }

    void FixedUpdate() {
        if (isIdle) {
            idleTimer += Time.deltaTime;
            if (idleTimer > idleTimerMax) {
                WhatDo();
            }
        }
        if (isWalking) {
            distTraveled += GAME.Shigellang_mvspd * Time.deltaTime;
            if (distTraveled > GAME.Shigellang_walkdist) {
                isWalking = false;
                isIdle = true;
                setVel(Vector2.zero);
                distTraveled = 0;
            }
        }

    }

    void WhatDo() {
        /*
        int rnglul = Random.Range(0, 100) % 4;
        switch (rnglul) {
            case 0:
                Walk();
                break;
            case 1:
                LeapAttack();
                break;
            case 2:
                Idle();
                break;
            case 3:
                LeapPlatform();
                break;
        }
        */
        LeapPlatform();
    }


    void LeapAttack() {
       
    }

    void Walk() {
        isWalking = true;
        isIdle = false;
        dirWalk *= -1;
        setVel(dirWalk);
    }

    void Idle() {
        idleTimer = 0;
        idleTimerMax = Random.Range(0.5f, GAME.Shigellang_TimeIdleRange);
    }
    void LeapPlatform() {
        Collider2D[] stuff = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 15f, mask);
        int rnglul = Random.Range(0, stuff.Length);
    }

    void ShootProjectile(Vector2 dir) {

    }

}
