using UnityEngine;
using System.Collections;
using GLOBAL;


public class ShigellangController : MonoBehaviour {
    public LayerMask mask_map;
	public LayerMask mask_player;
	public GameObject fire_position;
	public GameObject fire_projectile;
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
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leapTimer = leapAttackTimer = projectileSpewTimer = idleTimer = 0;
        dirWalk = Vector2.left;
        isWalking = false;
        isLeaping = false;
        isIdle = true;
		isLeapUp = false;
		isLeapAttacking = false;
        idleTimerMax = Random.Range(0.5f, GAME.Shigellang_TimeIdleRange);
        projectile_parent = GameObject.Find("Projectile Parent");
	}

    void setVel(Vector2 vel) {
        rb.velocity = vel;
    }

    void Update() {
        anim.SetFloat("Direction", dirWalk.x > 0 ? 1f : 0f);
        anim.SetBool("isWalking", isWalking);
    }

    void FixedUpdate() {
		player = Physics2D.OverlapCircle (rb.position, GAME.Shigellang_RadarPlayer,mask_player);
		if (player) {
			playerSpotted = true;
		} 
		else {
			playerSpotted = false;
		}


		if (isIdle) {
			idleTimer += Time.deltaTime;
			if (idleTimer > idleTimerMax) {
				WhatDo ();
			}
		} else if (isWalking) {
			distTraveled += GAME.Shigellang_mvspd * Time.deltaTime;
			if (distTraveled > GAME.Shigellang_walkdist) {
				isWalking = false;
				isIdle = true;
				setVel (Vector2.zero);
				distTraveled = 0;
			}
		} else if (isLeaping) {
			rb.position = Vector2.Lerp (transform.position, pos_leap, GAME.Shigellang_LeapSpeed * Time.deltaTime);
			float mag = (new Vector2 (pos_leap.x, pos_leap.y) - rb.position).magnitude;
			if (mag < 0.25f) {
				StopLeap ();
			}
		} else if (isLeapAttacking) {
			
		}
    }

    void WhatDo() {
		if (playerSpotted) {
			int rnglul = Random.Range (0, 100)%4;
			switch (rnglul) {
			case 0:
				Idle ();
				break;
			case 1:
				LeapAttack ();
				break;
			case 2:
				LeapAttack ();
				//ShootProjectile ();
				break;
			case 3:
				Walk ();
				break;
			}

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
		isLeapAttacking = true;
		isWalking = false;
		isLeaping = false;
		isIdle = false;
		dirWalk.x = player.transform.position.x - rb.position.x < 0 ? -1f : 1f;
		dirLeap = player.transform.position.x - rb.position.x < 0 ? Vector2.left : Vector2.right;
		dirLeap += Vector2.up * 1.5f;
		dirLeap = dirLeap.normalized;
		anim.SetTrigger ("leapAttack");
    }

	void LeapAttackJump(){
		rb.AddForce (dirLeap * 2f, ForceMode2D.Impulse);
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
		setVel (Vector2.zero);
        idleTimerMax = Random.Range(0.5f, GAME.Shigellang_TimeIdleRange);
    }
    void LeapPlatform() {
		Collider2D[] stuff = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), GAME.Shigellang_RadarMap, mask_map);
        int rnglul = Random.Range(0, stuff.Length);

        if(currOnTop != null && currOnTop == stuff[rnglul]) {
            Debug.Log(rnglul);
            Debug.Log("change");
            rnglul = (rnglul + 1) % stuff.Length;
            Debug.Log(rnglul);
            
        }
        currOnTop = stuff[rnglul];
        pos_leap = currOnTop.bounds.center + Vector3.up * (currOnTop.bounds.extents.y + 1f);
        dirLeap = (new Vector2(pos_leap.x, pos_leap.y) - rb.position + Vector2.up * currOnTop.bounds.extents.y).normalized;
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
    }

	void StopLeap(){
		rb.isKinematic = false;
		anim.SetTrigger ("landing");
		Idle ();
	}

    void ShootProjectile() {
		anim.SetTrigger ("projectileAttack");
		setVel (Vector2.zero);
		dirWalk.x = player.transform.position.x - rb.position.x < 0 ? -1f : 1f;
    }

	void Fire(){
		GameObject temp = Instantiate(fire_projectile,fire_position.transform.position,Quaternion.identity) as GameObject;
		temp.GetComponent<MoveDir>().setDir((player.transform.position - fire_position.transform.position).normalized);
		temp.transform.parent = projectile_parent.transform;
	}

	void OnTriggerEnter2D(Collider2D other){
		//other.GetComponent<PlayerHealth> ().TakeDamage ();
	}



}
