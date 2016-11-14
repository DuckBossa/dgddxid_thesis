using UnityEngine;
using System.Collections;
using System;
using GLOBAL;

public class PlayerAttack : MonoBehaviour {
    int weapon_switch;
	int[] weapon_level;
    bool isAttacking,attackCalled;
    Animator anim;
	PlayerMovement pm;
	Rigidbody2D rb;
    void Start() {
		pm = GetComponent<PlayerMovement> ();
		rb = GetComponent<Rigidbody2D> ();
		weapon_level = new int[GAME.num_swords];
		attackCalled = false;
		weapon_switch = 0;
		for (int i = 0; i < weapon_level.Length; i++) {
			weapon_level [i] = 1;
		}
		anim = GetComponent<Animator>();
        isAttacking = false;
    }

    public void OnTriggerEnter2D(Collider2D other) {
		other.gameObject.GetComponent<EnemyHealth>().TakeDamage(weapon_level[weapon_switch]);
    }

    void FixedUpdate() {
		if (attackCalled && !pm.isDash ())
			Attack ();
        anim.SetBool("isAttacking", isAttacking);
    }

    void LateUpdate() {
        if (isAttacking) {
			if (pm.isFall () || pm.isJump ()) {
				
			} 
			else {
			
			}
            var subSprites = Resources.LoadAll<Sprite>(GAME.character_weapons_folder + GAME.character_weapon_swords[weapon_switch]);
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
                string spriteName = renderer.sprite.name;
                var newSprite = Array.Find(subSprites, item => item.name == spriteName);
                if (newSprite)
                    renderer.sprite = newSprite;
            }
        }

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
			weapon_switch = (weapon_switch + 1) % GAME.character_weapon_swords.Length/2;
		}
	}

	public void SetAttack(bool a) {
		attackCalled = a;
	}

}
