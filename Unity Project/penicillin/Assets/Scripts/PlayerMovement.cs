using UnityEngine;
using System.Collections;
using GLOBAL;
public class PlayerMovement : MonoBehaviour {
  
    float dir;
	float dashTimer;
    Animator anim;
    Rigidbody2D rb;
    int currjumps;
    int currdash;
    bool isWalking;
    bool isAttacking;
    bool isDashing;
    bool isJumping;
    bool isFalling;
    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currjumps = GAME.jumps;
		currdash = GAME.dashes;
		dir = 0;
		dashTimer = 0;
		isWalking = false;
        isAttacking = false;
        isDashing = false;
        isJumping = false;
        isDashing = false;

    }

	void Update(){
		if (currdash < GAME.dashes) {
			dashTimer += Time.deltaTime;
		}
		if (dashTimer > GAME.dash_cooldown && currdash < GAME.dashes) {
			dashTimer = 0;
			currdash++; 
		}
	}


    void FixedUpdate() {
        dir = Input.GetAxisRaw("Horizontal");
        bool jumping = Input.GetKeyDown(KeyCode.W);
		if(Input.GetKeyDown(KeyCode.Z) && !isDashing){
			isDashing = true;
			Dash ();
		}
        isDashing = Input.GetKeyDown(KeyCode.Z);
        isWalking = Mathf.Abs(dir) > 0;
        if (jumping && currjumps > 0) {
            Jump();
            isJumping = true;
        }
		if(rb.velocity.y < 0) {
            isFalling = true;
        }
        else {
            isFalling = false;
        }

		if (isWalking && !isDashing) {
			rb.velocity = new Vector2 (dir * GAME.player_velocity, rb.velocity.y);
		} 
		else if (!isDashing) {
			rb.velocity = new Vector2(0, rb.velocity.y);
		}



		anim.SetFloat("Direction", dir);
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isDashing", isDashing);
		anim.SetBool("isFalling", isFalling);
    }

    void OnCollisionEnter2D(Collision2D coll){
        
        bool onTop = coll.transform.position.y > transform.position.y;
        if (!onTop){
            currjumps = GAME.jumps;
            isJumping = false;
        }
            
    }

    void Jump() {
		rb.AddForce(Vector2.up * GAME.jump_force);
        currjumps--;
    }

	void Dash(){
		if (currdash > 0) {
			rb.AddForce (new Vector2 (dir * GAME.dash_force, 0), ForceMode2D.Impulse);
			currdash--;
		}

	}

	void stopDash(){
		isDashing = true;
	}
}
