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
    float jumptime;
    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currjumps = GAME.jumps;
        dir = 0;
        isWalking = false;
        isAttacking = false;
        isDashing = false;
        isJumping = false;
        jumptime = 0f;
    }

    void Update(){
        dir = Input.GetAxisRaw("Horizontal");
        bool jumping = Input.GetKeyDown(KeyCode.W);
        bool dashing = Input.GetKeyDown(KeyCode.Z);
        isWalking = Mathf.Abs(dir) > 0;

        if (isJumping){
            jumptime += Time.deltaTime;
            if(jumptime > GAME.jump_anim_loop){
                anim.SetTime(GAME.jump_anim_loop);
                jumptime = 0.9f;
            }
        }

        if (jumping && currjumps > 0){
            Jump();
            isJumping = true;
        }


        

        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isJumping", isJumping);
        if (isWalking){
            anim.SetFloat("Direction", dir);
            Vector3 movement = new Vector3(dir * GAME.player_velocity * Time.deltaTime, 0, 0);
            transform.Translate(movement);
        }


    }
    void OnCollisionEnter2D(Collision2D coll){
        
        bool onTop = coll.transform.position.y > transform.position.y;
        if (!onTop){
            currjumps = GAME.jumps;
            isJumping = false;
            jumptime = 0;
        }
            
    }

    void Jump() {
        rb.AddForce(Vector2.up * GAME.jump_force);
        currjumps--;
    }

}
