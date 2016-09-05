using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public const float VELOCITY = 5f;
    float dir;
    Animator anim;
    

    void Start(){
        anim = GetComponent<Animator>();
        dir = 0;
    }

    void Update(){
        dir = Input.GetAxis("Horizontal");
        anim.SetFloat("Direction", dir);
    }


}
