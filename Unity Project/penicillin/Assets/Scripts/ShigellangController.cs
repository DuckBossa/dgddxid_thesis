﻿using UnityEngine;
using System.Collections;
using GLOBAL;


public class ShigellangController : MonoBehaviour {
    public LayerMask mask_map;
    public LayerMask mask_player;
    public GameObject fire_position;
    public GameObject fire_projectile;
    public int currHealth;
    GameObject projectile_parent;
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
    Vector3 pos_leap;
    bool isIdle;
    bool isWalking;
    bool isLeaping;
    bool isLeapAttacking;
    bool playerSpotted;
    bool isLeapUp;
    Collider2D player;
    Collider2D currOnTop;


    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leapTimer = leapAttackTimer = projectileSpewTimer = idleTimer = 0;
        dirWalk = Vector2.left;
        isWalking = false;
        isLeaping = false;
        isIdle = true;
        isLeapUp = false;
        isLeapAttacking = false;
        anim.SetBool("isMidair", false);
        idleTimerMax = Random.Range(0.5f, GAME.Shigellang_TimeIdleRange);
        projectile_parent = GameObject.Find("Projectile Parent");
        currHealth = GAME.Shigellang_Fighting_MaxHealth;
    }

    void setVel(Vector2 vel) {
        rb.velocity = vel;
    }

    void Update() {
        anim.SetFloat("Direction", dirWalk.x > 0 ? 1f : 0f);
        anim.SetBool("isWalking", isWalking);
    }

    void FixedUpdate() {
        player = Physics2D.OverlapCircle(rb.position, GAME.Shigellang_RadarPlayer, mask_player);
        if (player) {
            playerSpotted = true;
        }
        else {
            playerSpotted = false;
        }


        if (isIdle) {
            idleTimer += Time.deltaTime;
            if (idleTimer > idleTimerMax) {
                WhatDo();
            }
        }
        else if (isWalking) {
            distTraveled += GAME.Shigellang_mvspd * Time.deltaTime;
            if (distTraveled > GAME.Shigellang_walkdist) {
                isWalking = false;
                isIdle = true;
                setVel(Vector2.zero);
                distTraveled = 0;
            }
        }
        else if (isLeaping) {
            rb.position = Vector2.Lerp(transform.position, pos_leap, GAME.Shigellang_LeapSpeed * Time.deltaTime);
            float mag = (new Vector2(pos_leap.x, pos_leap.y) - rb.position).magnitude;
            if (mag < 0.25f) {
                StopLeap();
            }
        }
        else if (isLeapAttacking) {
        }
    }

    void WhatDo() {
        if (playerSpotted) {
            int rnglul = Random.Range(0, 100) % 4;
            switch (rnglul) {
                case 0:
                    Idle();
                    break;
                case 1:
                    float dist = (new Vector2(0, player.transform.position.y) - new Vector2(0, transform.position.y)).magnitude;
                    if (dist > 1.5f)
                        ShootProjectile();
                    else
                        LeapAttack();
                    break;
                case 2:

                    ShootProjectile();
                    break;
                case 3:
                    Walk();
                    break;
            }

        }
        else {
            int rnglul = Random.Range(0, 100) % 3;
            switch (rnglul) {
                case 0:
                    Idle();
                    break;
                case 1:
                    Walk();
                    break;
                case 2:
                    LeapPlatform();
                    break;
            }

        }
    }

    void LeapAttack() {
        isLeapAttacking = true;
        isWalking = false;
        isLeaping = false;
        isIdle = false;
        dirWalk.x = player.transform.position.x - rb.position.x < 0 ? -1f : 1f;
        dirLeap = player.transform.position.x - rb.position.x < 0 ? Vector2.left : Vector2.right;
        dirLeap += Vector2.up * 1.5f;
        dirLeap = dirLeap.normalized;
        pos_leap = player.transform.position;
        anim.SetBool("isMidair", true);
        anim.SetTrigger("leapAttack");
    }

    void LeapAttackJump() {
        float mag = (pos_leap - transform.position).magnitude;
        rb.AddForce(dirLeap * mag * 2, ForceMode2D.Impulse);
    }

    void LeapAttackStop() {
        Idle();
    }
    void Walk() {
        isWalking = true;
        isIdle = false;
        isLeaping = false;
        isLeapAttacking = false;
        dirWalk *= -1;
        setVel(dirWalk);
    }

    void Idle() {
        idleTimer = 0;
        isIdle = true;
        isLeapAttacking = false;
        isWalking = false;
        isLeaping = false;
        setVel(Vector2.zero);
        idleTimerMax = Random.Range(0.35f, GAME.Shigellang_TimeIdleRange);
    }
    void LeapPlatform() {
        Collider2D[] stuff = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), GAME.Shigellang_RadarMap, mask_map);
        int rnglul = Random.Range(0, stuff.Length);

        if (currOnTop != null && currOnTop == stuff[rnglul]) {
            rnglul = (rnglul + 1) % stuff.Length;
        }
        currOnTop = stuff[rnglul];
        pos_leap = currOnTop.bounds.center + Vector3.up * (currOnTop.bounds.extents.y + 1f);
        dirLeap = (new Vector2(pos_leap.x, pos_leap.y) - rb.position + Vector2.up * currOnTop.bounds.extents.y).normalized;
        isWalking = false;
        isIdle = false;
        isLeapAttacking = false;
        isLeapUp = pos_leap.y > transform.position.y;
        dirWalk.x = dirLeap.x > 0 ? 1 : -1;
        anim.SetTrigger("leap");

    }

    void MoveLeap() {
        rb.isKinematic = true;
        isLeaping = true;
    }

    void StopLeap() {
        rb.isKinematic = false;
        anim.SetTrigger("landing");
        Idle();
    }

    void ShootProjectile() {
        anim.SetTrigger("projectileAttack");
        setVel(Vector2.zero);
        dirWalk.x = player.transform.position.x - rb.position.x < 0 ? -1f : 1f;
    }

    void Fire() {
        GameObject temp = Instantiate(fire_projectile, fire_position.transform.position, Quaternion.identity) as GameObject;
        temp.GetComponent<MoveDir>().setDir((player.transform.position - fire_position.transform.position).normalized);
        temp.transform.parent = projectile_parent.transform;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("triggered");
        //other.GetComponent<PlayerHealth> ().TakeDamage ();
    }

    public void SetBottom(Collider2D tile) {
        currOnTop = tile;
    }

    public void TakeDamage(int dmg) {
        currHealth -= dmg;
        if (currHealth < 0) {
            Death();
        }
    }

    public void Death() {
        anim.SetTrigger("dead");
        Destroy(gameObject, 0.75f);
    }
}
