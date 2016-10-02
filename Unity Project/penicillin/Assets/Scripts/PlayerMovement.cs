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
    public bool isJumping;
    bool isFalling;

    Transform trans, groundCheck;
    public LayerMask playerMask;
    float hInput = 0;

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

        trans = this.transform;
        groundCheck = GameObject.Find(this.name + "/GroundCheck").transform;
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
        //move
        Move(hInput);

        //jump (spacebar now)
        isJumping = !(Physics2D.Linecast(trans.position, groundCheck.position, playerMask));
        if(!isJumping) currjumps = GAME.jumps;

        //fall
        isFalling = rb.velocity.y < 0 ? true : false;
        isWalking = Mathf.Abs(dir) > 0;
        anim.SetFloat("Direction", dir);
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isDashing", isDashing);
		anim.SetBool("isFalling", isFalling);
    }

    public void Jump() {
        if(!isJumping) rb.velocity += GAME.jump_velocity * Vector2.up;
    }

    void Move(float horizontalInput) {
        //if (isJumping) return;
        dir = horizontalInput;
        isWalking = Mathf.Abs(dir) > 0;
        Vector2 myVel = rb.velocity;
        myVel.x = horizontalInput * GAME.player_velocity;
        rb.velocity = myVel;
    }

    public void MoveStart(float horizontalInput)
    {
        hInput = horizontalInput;
    }

	public void Dash(){
		if (currdash > 0) {
			rb.AddForce (new Vector2 (dir * GAME.dash_force, 0), ForceMode2D.Impulse);
			currdash--;
		}
        
	}

	void stopDash(){
		isDashing = true;
	}

    public void Attack() {
        
    }
}
