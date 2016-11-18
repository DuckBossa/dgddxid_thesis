using UnityEngine;
using System.Collections;
using GLOBAL;


public class ShigellangController : MonoBehaviour {
    public LayerMask mask_map;
	public LayerMask mask_player;

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
    bool playerSpotted;
	bool isLeapUp;
	Collider2D player;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leapTimer = leapAttackTimer = projectileSpewTimer = idleTimer = 0;
        dirWalk = Vector2.left;
        isWalking = false;
        isLeaping = false;
        isIdle = true;
		isLeapUp = false;
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
		player = Physics2D.OverlapCircle (rb.position, 15f,mask_player);
		if (player) {
			playerSpotted = true;
			Debug.Log ("Spot");
		} 
		else {
			playerSpotted = false;
			Debug.Log ("No Spot");
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
			rb.position = Vector2.Lerp (transform.position, pos_leap, GAME.Shigellang_LeapSpeed * Time.deltaTime);
			float mag = (new Vector2 (pos_leap.x, pos_leap.y) - rb.position).magnitude;
			Debug.Log (mag);
			if (mag < 0.3f) {
				StopLeap ();
			}
		}

    }

    void WhatDo() {

		if (playerSpotted) {
		} 
		else {
			int rnglul = Random.Range (0, 100) % 3;
			switch (rnglul) {
			case 0:
				Idle ();
				break;
			case 1:
				Walk ();
				break;
			case 2:
				LeapPlatform ();
				break;
			}
		
		}
			

       
    }


    void LeapAttack() {
		Idle ();
    }

    void Walk() {
        isWalking = true;
        isIdle = false;
		isLeaping = false;
        dirWalk *= -1;
        setVel(dirWalk);
    }

    void Idle() {
        idleTimer = 0;
		isIdle = true;
		isWalking = false;
		isLeaping = false;
        idleTimerMax = Random.Range(0.5f, GAME.Shigellang_TimeIdleRange);
    }
    void LeapPlatform() {
		Collider2D[] stuff = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 30f, mask_map);
        int rnglul = Random.Range(0, stuff.Length);
		pos_leap = stuff[rnglul].bounds.center + Vector3.up * (stuff [rnglul].bounds.extents.y + 1f); 
		dirLeap = (new Vector2(pos_leap.x, pos_leap.y) - rb.position + Vector2.up * stuff[rnglul].bounds.extents.y).normalized;
        //if player can be seen, go to the platform nearest the player
        //if not, go to a platorm that is not the nearest
        isWalking = false;
        isIdle = false;
		isLeapUp = pos_leap.y > transform.position.y;


        dirWalk.x = dirLeap.x > 0 ? 1 : -1;
		anim.SetTrigger ("leap");

    }

    void MoveLeap() {
		rb.isKinematic = true;
		isLeaping = true;
		//rb.velocity = dirLeap * GAME.Shigellang_LeapSpeed;
		//rb.AddForce (Vector2.up * GAME.Shigellang_JumpForce, ForceMode2D.Impulse);
    }

	void StopLeap(){
		rb.isKinematic = false;
		//rb.velocity = Vector2.zero;
		anim.SetTrigger ("landing");
		Idle ();
	}

    void ShootProjectile(Vector2 dir) {

    }

}
