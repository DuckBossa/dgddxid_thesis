using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
  
    float dir;
    Animator anim;
    Rigidbody2D rb;

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        dir = 0;
    }

    void Update(){
        dir = Input.GetAxis("Horizontal");
        anim.SetFloat("Direction", dir);

        

    }


}
