using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GLOBAL;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    public bool isJumping, canAttack;
    public LayerMask playerMask;
    public Slider dashSlider;

    Animator anim;
    Rigidbody2D rb;
    int currjumps, currdash;
    bool isWalking, isAttacking, isDashing, isFalling, faceRight;
    Transform trans, groundCheck;
    float dir, dashCDTimer, dashTimer, attackTimer;
    float hInput = 0;
    float timer;
    int weapon_switch;

    void Start() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currjumps = GAME.jumps;
        currdash = GAME.dashes;
        dir = 0;
        dashCDTimer = 0;
        dashTimer = 0;
        weapon_switch = 0;
        dashSlider.maxValue = GAME.dashes;
        dashSlider.value = GAME.dashes;

        isWalking = false;
        isAttacking = false;
        isDashing = false;
        isJumping = false;
        isDashing = false;
        canAttack = false;
        faceRight = true;

        trans = this.transform;
        groundCheck = GameObject.Find(this.name + "/GroundCheck").transform;
    }

    void Update() {
        timer += Time.deltaTime;
        if (currdash < GAME.dashes) {
            dashCDTimer += Time.deltaTime;
        }
        if (dashCDTimer > GAME.dash_cooldown && currdash < GAME.dashes) {
            dashCDTimer = 0;
            currdash++;
            dashSlider.value = currdash;
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
        //attack
        if (canAttack && !isDashing) Attack();

        //move
        Move(hInput);

        //jump
        isJumping = !(Physics2D.Linecast(trans.position, groundCheck.position, playerMask));
        if (!isJumping) currjumps = GAME.jumps;


        //fall
        isFalling = rb.velocity.y < 0 ? true : false;
        isWalking = Mathf.Abs(dir) > 0 && !isDashing;
        anim.SetFloat("Direction", faceRight ? 1f : 0f);
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isFalling", isFalling);
        anim.SetBool("isAttacking", isAttacking);
    }

    public void Jump() {
        if (!isJumping) rb.velocity += GAME.jump_velocity * Vector2.up;
    }

    public bool amDashing() {
        return isDashing;
    }

    void Move(float horizontalInput) {
        //if (isJumping) return;
        if (!isDashing && !isAttacking) {
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
        if (!isDashing && !isAttacking) {
            if (currdash > 0) {
                rb.AddForce(new Vector2((faceRight ? 1 : -1) * GAME.dash_force, 0), ForceMode2D.Impulse);
                currdash--;
                dashSlider.value = currdash;
                isDashing = true;

            }
        }

    }

    void endAttack() {
        isAttacking = false;
    }

    void stopDash() {
        isDashing = false;
        anim.SetBool("isDashing", isDashing);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    void LateUpdate() {
        if (isAttacking) {
            var subSprites = Resources.LoadAll<Sprite>(GAME.character_weapons_folder + GAME.character_weapon_swords[weapon_switch]);
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
                string spriteName = renderer.sprite.name;
                var newSprite = Array.Find(subSprites, item => item.name == spriteName);
                if (newSprite)
                    renderer.sprite = newSprite;
            }
        }

    }

    public void Attack() {
        if (!isDashing && !isFalling && !isJumping && !isAttacking) {
            rb.velocity = new Vector2(0, rb.velocity.y);
            isAttacking = true;
            RaycastHit2D hit;
            if (faceRight) hit = Physics2D.Raycast(new Vector2(trans.position.x + .2f, trans.position.y - .1f), Vector2.right, GAME.player_atkRange, LayerMask.NameToLayer("Enemy"));
            else hit = Physics2D.Raycast(new Vector2(trans.position.x - .2f, trans.position.y - .1f), Vector2.left, GAME.player_atkRange);
            if (hit.collider != null) {
                timer = 0;
                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage();
            }
        }
    }

    public void SwitchWeapon() {
        if (!isAttacking) {
            weapon_switch = (weapon_switch + 1) % GAME.character_weapon_swords.Length;
        }
    }


    public void SetAttack(bool a) {
        canAttack = a;
    }
}
