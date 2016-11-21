using UnityEngine;
using System.Collections;

public class LandingScript : MonoBehaviour {

    Animator anim;
	void Start () {
        anim = transform.parent.gameObject.GetComponent<Animator>();
	}
	
    void OnCollisionEnter2D(Collision2D other) {
        anim.SetBool("isMidair",false);
        transform.parent.gameObject.GetComponent<ShigellangController>().SetBottom(other.collider);
    }
}
