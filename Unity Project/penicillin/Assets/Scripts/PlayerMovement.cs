using UnityEngine;
using System.Collections;
using GLOBAL;
public class PlayerMovement : MonoBehaviour {
  
    float dir;
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
        dir = 0;
        isWalking = false;
        isAttacking = false;
        isDashing = false;
        isJumping = false;
        isDashing = false;
    }

    void FixedUpdate() {
        dir = Input.GetAxisRaw("Horizontal");
        bool jumping = Input.GetKeyDown(KeyCode.W);
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
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isFalling", isFalling);
        if (isWalking) {
            anim.SetFloat("Direction", dir);
            rb.velocity = new Vector2(dir * GAME.player_velocity, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
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

}
