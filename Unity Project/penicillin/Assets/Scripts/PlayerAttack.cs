using UnityEngine;
using System.Collections;
using System;
using GLOBAL;

public class PlayerAttack : MonoBehaviour {
    int whichWeapon;
	int[] weapLevel;
    bool isAttacking,attackCalled;
    Animator anim;
	PlayerMovement pm;
	Rigidbody2D rb;
    void Start() {
		pm = GetComponent<PlayerMovement> ();
		rb = GetComponent<Rigidbody2D> ();
		weapLevel = new int[GAME.num_swords];
		attackCalled = false;
		whichWeapon = 0;
		for (int i = 0; i < weapLevel.Length; i++) {
			weapLevel [i] = i;
		}
		anim = GetComponent<Animator>();
        isAttacking = false;
    }

    public void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<EnemyHealth>().TakeDamage(weapLevel[whichWeapon] + 1);
    }

    void FixedUpdate() {
		if (attackCalled && !pm.isDash ())
			Attack ();
        anim.SetInteger("whichWeapon", whichWeapon);
        anim.SetInteger("weapLevel", weapLevel[whichWeapon]);
        anim.SetBool("isAttacking", isAttacking);
    }

    public bool isAttack() {
        return isAttacking;
    }

    void endAttack() {
        isAttacking = false;
    }


	public void Attack() {
		if ( !pm.isDash() && !isAttacking) {
			rb.velocity = new Vector2(0, rb.velocity.y);
			isAttacking = true;
		}
	}
	public void SwitchWeapon() {
		if (!isAttacking) {
            whichWeapon = (whichWeapon + 1) % GAME.num_swords;
        }
	}

	public void SetAttack(bool a) {
		attackCalled = a;
	}

}
