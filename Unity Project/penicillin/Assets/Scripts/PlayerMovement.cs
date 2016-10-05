using UnityEngine;
using System.Collections;
using GLOBAL;

public class PlayerMovement : MonoBehaviour {
  
    float dir, dashTimer;
    Animator anim;
    Rigidbody2D rb;
    int currjumps, currdash;
    bool isWalking, isAttacking, isDashing, isFalling, faceRight;
    public bool isJumping, canAttack;

    Transform trans, groundCheck;
    public LayerMask playerMask;
    float hInput = 0;
    float timer, attackAnimationLength;
    public Enemy enemy;

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
        canAttack = false;

        trans = this.transform;
        groundCheck = GameObject.Find(this.name + "/GroundCheck").transform;
    }

    void Update(){
        timer += Time.deltaTime;
        //if (isAttacking && attackAnimationLength > Time.deltaTime) isAttacking = false;
		if (currdash < GAME.dashes) {
			dashTimer += Time.deltaTime;
		}
		if (dashTimer > GAME.dash_cooldown && currdash < GAME.dashes) {
			dashTimer = 0;
			currdash++; 
		}
	}

    void FixedUpdate() {
        //attack
        if (canAttack) Attack();

        //move
        Move(hInput);

        //jump (spacebar now)
        isJumping = !(Physics2D.Linecast(trans.position, groundCheck.position, playerMask));
        if(!isJumping) currjumps = GAME.jumps;

        //fall
        isFalling = rb.velocity.y < 0 ? true : false;
        isWalking = Mathf.Abs(dir) > 0;
        anim.SetFloat("Direction", faceRight ? 1f : 0f);
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
        if (myVel.x > 0 || myVel.x < 0) faceRight = myVel.x > 0;
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
        if (timer > GAME.timeBetweenAttacks) {
            //isAttacking = true;
            //anim.SetBool("isAttacking", isAttacking);
            RaycastHit2D hit;
            if (faceRight) hit = Physics2D.Raycast(new Vector2(trans.position.x + .2f, trans.position.y - .1f), Vector2.right, GAME.player_atkRange);
            else hit = Physics2D.Raycast(new Vector2(trans.position.x - .2f, trans.position.y - .1f), Vector2.left, GAME.player_atkRange);
            if (hit.collider != null) {
                timer = 0;
                //Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage();
            }
        }
    }

    public void SetAttack(bool a) {
        canAttack = a;
    }
}
