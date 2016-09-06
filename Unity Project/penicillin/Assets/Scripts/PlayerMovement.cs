using UnityEngine;
using System.Collections;
using GLOBAL;
public class PlayerMovement : MonoBehaviour {
  
    float dir;
    Animator anim;
    Rigidbody2D rb;
    int currjumps;
    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currjumps = GAME.jumps;
        dir = 0;
    }

    void Update(){
        dir = Input.GetAxis("Horizontal");
        bool jumping = Input.GetKeyDown(KeyCode.W);
        anim.SetFloat("Direction", dir);
        Vector3 movement = new Vector3(dir * GAME.player_velocity * Time.deltaTime, 0, 0);
        transform.Translate(movement);
        if(jumping && currjumps > 0){
            Jump();
        }

    }
    void OnCollisionEnter2D(Collision2D coll){
        
        bool onTop = coll.transform.position.y > transform.position.y;
        if (!onTop){
            currjumps = GAME.jumps;
        }
            
    }

    void Jump() {
        rb.AddForce(Vector2.up * GAME.jump_force);
        currjumps--;
    }

}
