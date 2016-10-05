using UnityEngine;
using System.Collections;
using GLOBAL;

public class PlayerMovement : MonoBehaviour {
    public bool isJumping, canAttack;
    public LayerMask playerMask;
    public Enemy enemy;

    Animator anim;
    Rigidbody2D rb;
    int currjumps, currdash;
    bool isWalking, isAttacking, isDashing, isFalling, faceRight;
    Transform trans, groundCheck;
    float dir, dashCDTimer, dashTimer;
    float hInput = 0;
    float timer, attackAnimationLength;


    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currjumps = GAME.jumps;
		currdash = GAME.dashes;
		dir = 0;
		dashCDTimer = 0;
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
			dashCDTimer += Time.deltaTime;
		}
		if (dashCDTimer > GAME.dash_cooldown && currdash < GAME.dashes) {
			dashCDTimer = 0;
			currdash++; 
		}

        if (isDashing) {
            dashTimer += Time.deltaTime;
            if(dashTimer > GAME.dash_timer) {
                stopDash();
                dashTimer = 0;
                Debug.Log("Reset");
            }
        }
    }

    void FixedUpdate() {
        //attack
        if (canAttack && !isDashing) Attack();

        //move
        Move(hInput);

        //jump
        isJumping = !(Physics2D.Linecast(trans.position, groundCheck.position, playerMask));
        if(!isJumping) currjumps = GAME.jumps;


        //fall
        isFalling = rb.velocity.y < 0 ? true : false;
        isWalking = Mathf.Abs(dir) > 0 && !isDashing;
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
        if (!isDashing) {
            dir = horizontalInput;
            isWalking = Mathf.Abs(dir) > 0;
            Vector2 myVel = rb.velocity;
            myVel.x = horizontalInput * GAME.player_velocity;
            if (myVel.x > 0 || myVel.x < 0) faceRight = myVel.x > 0;
            rb.velocity = myVel;
        }

    }

    public void MoveStart(float horizontalInput)
    {
        hInput = horizontalInput;
    }

	public void Dash(){
        if (!isDashing) {
            if (currdash > 0) {
                rb.AddForce(new Vector2((faceRight ? 1 : -1) * GAME.dash_force, 0), ForceMode2D.Impulse);
                currdash--;
                isDashing = true;
            }
        }
	
	}

	void stopDash(){
		isDashing = false;
        anim.SetBool("isDashing", isDashing);
        rb.velocity = new Vector2(0, rb.velocity.y);
	}

    public void Attack() {
        if (timer > GAME.timeBetweenAttacks && !isDashing && !isFalling && !isJumping) {
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
