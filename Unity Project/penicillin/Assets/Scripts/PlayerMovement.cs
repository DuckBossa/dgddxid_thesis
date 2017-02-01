using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GLOBAL;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {
    public LayerMask playerMask;
    public Sprite dash_empty, dash_one, dash_two, dash_three;
    public Image dashImage, dashCooldown;

    public float hInput = 0;
    Animator anim;
    Rigidbody2D rb;
    PlayerAttack pa;
    int currjumps, currdash;
	bool isWalking, isDashing, isFalling, faceRight,isJumping;
    Transform trans, groundCheckL, groundCheckR;
    float dir, dashCDTimer, dashTimer, attackTimer;
    float timer;

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pa = GetComponent<PlayerAttack>();
        currjumps = GAME.jumps;
        currdash = GAME.dashes;
        dir = 0;
        dashCDTimer = 0;
        dashTimer = 0;
        isWalking = false;
        isDashing = false;
        isJumping = false;
        isDashing = false;
        faceRight = true;
        trans = this.transform;
        groundCheckL = GameObject.Find(this.name + "/GroundCheckLeft").transform;
        groundCheckR = GameObject.Find(this.name + "/GroundCheckRight").transform;
    }

    void Update() {
        timer += Time.deltaTime;
        if (currdash < GAME.dashes) {
            dashCDTimer += Time.deltaTime;
            dashCooldown.fillAmount = 1f - (dashCDTimer / (float)GAME.dash_cooldown);
        }
        if (dashCDTimer > GAME.dash_cooldown && currdash < GAME.dashes) {
            dashCDTimer = 0;
            currdash++;
            if (currdash == 1) dashImage.sprite = dash_one;
            else if (currdash == 2) dashImage.sprite = dash_two;
            else if (currdash == 3) dashImage.sprite = dash_three;
        }

        if (isDashing) {
            dashTimer += Time.deltaTime;
            if (dashTimer > GAME.dash_timer) {
                stopDash();
                dashTimer = 0;
            }
        }

    }

    void FixedUpdate() {
        //Move(CrossPlatformInputManager.GetAxisRaw("Horizontal"));
        Move(hInput);

        //jump
        isJumping = !(Physics2D.Linecast(new Vector3(groundCheckL.position.x, groundCheckL.position.y + .1f, groundCheckL.position.z), groundCheckL.position, playerMask) || 
                      Physics2D.Linecast(new Vector3(groundCheckR.position.x, groundCheckR.position.y + .1f, groundCheckR.position.z), groundCheckR.position, playerMask));
        Debug.DrawLine(new Vector3(groundCheckL.position.x, groundCheckL.position.y + .1f, groundCheckL.position.z), groundCheckL.position);
        Debug.DrawLine(new Vector3(groundCheckR.position.x, groundCheckR.position.y + .1f, groundCheckR.position.z), groundCheckR.position);
        if (!isJumping) currjumps = GAME.jumps;


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
        if (!isJumping) rb.velocity += GAME.jump_velocity * Vector2.up;
    }

    public bool amDashing() {
        return isDashing;
    }

    void Move(float horizontalInput) {
		if (!isDashing && !pa.isAttack()) {
            dir = horizontalInput;
            isWalking = Mathf.Abs(dir) > 0;
            Vector2 myVel = rb.velocity;
            myVel.x = horizontalInput * GAME.player_velocity;
            if (myVel.x > 0 || myVel.x < 0) faceRight = myVel.x > 0;
            rb.velocity = myVel;
        }
    }

    public void MoveStart(float horizontalInput) {
        hInput = horizontalInput;
    }

    public void Dash() {
		if (!isDashing && !pa.isAttack()) {
            if (currdash > 0) {
                rb.AddForce(new Vector2((faceRight ? 1 : -1) * GAME.dash_force, 0), ForceMode2D.Impulse);
                currdash--;
                if (currdash == 1) dashImage.sprite = dash_one;
                else if (currdash == 2) dashImage.sprite = dash_two;
                else if (currdash == 0) dashImage.sprite = dash_empty;
                isDashing = true;
            }
        }

    }

 

    void stopDash() {
        isDashing = false;
        anim.SetBool("isDashing", isDashing);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
		
	public bool isFall(){
		return isFalling;
	}

	public bool isJump(){
		return isJumping;
	}
	public bool isDash(){
		return isDashing;
	}
}
