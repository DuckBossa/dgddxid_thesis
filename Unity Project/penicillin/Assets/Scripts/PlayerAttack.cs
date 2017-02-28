using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using GLOBAL;

public class PlayerAttack : MonoBehaviour,IPlayerDamage {
    int whichWeapon;
	int swordDurr;
	int[] weapLevel = new int[GAME.num_weapons];
    bool isAttacking,attackCalled;
    bool hasAmmo;
    Animator anim;
	PlayerMovement pm;
	Rigidbody2D rb;
    public Sprite[] weapsp;
    public GameObject sw; /* button for switching weapons */



    void Awake() {
		pm = GetComponent<PlayerMovement> ();
		rb = GetComponent<Rigidbody2D> ();
        hasAmmo = true;
		attackCalled = false;
		whichWeapon = 0;
		weapLevel [0] = 0;
		for (int i = 0; i < weapLevel.Length; i++) {
			weapLevel [i] = -1;
			//PlayerPrefs.GetInt(GAME.PLAYER_PREFS_WEAPLEVEL + i.ToString(),0);
		}
		anim = GetComponent<Animator>();
		swordDurr = GAME.WEAP_DURABILITY [0, 0];
        isAttacking = false;
        sw.GetComponent<Image>().sprite = weapsp[0];
    }

    /*
    public void OnTriggerEnter2D(Collider2D other) {
        //other.gameObject.GetComponent<IDamage>().TakeDamage(weapLevel[whichWeapon] + 1);
	
		if(other.gameObject.layer == LayerMask.NameToLayer ("Enemy"))
			other.gameObject.transform.parent.gameObject.GetComponent<IDamage> ().TakeDamage (weapLevel [whichWeapon] + 1);
	
	}

   */

    void FixedUpdate() {
		if (attackCalled && !pm.isDash ())
			Attack ();
        anim.SetBool("isAttacking", isAttacking);
    }

    public bool isAttack() {
        return isAttacking;
    }

	public int GetWeapLevel(int weapID){
		if (weapID < weapLevel.Length) {
			return weapLevel [weapID];
		}
		return -1;
	}

	public void SaveData(){
		for (int i = 0; i < weapLevel.Length; i++) {
			PlayerPrefs.SetInt(GAME.PLAYER_PREFS_WEAPLEVEL + i.ToString(),weapLevel [i]);
		}
	}

	public void UpgradeWeapon(int id){
		if (weapLevel [id] < 2) {
			++weapLevel [id];
		}
		switch (id) {
		case 0:
			swordDurr = GAME.WEAP_DURABILITY [id, weapLevel [id]];
			break;
		case 1:
			GetComponent<PlayerDaggerShoots> ().ReplenishAmmo ();
			break;
		case 2:
			GetComponent<PlayerClusterBombs> ().ReplenishAmmo ();
			break;
		}
        anim.SetInteger("whichWeapon", whichWeapon);
        anim.SetInteger("weapLevel", weapLevel[whichWeapon]);
    }

    void endAttack() {
        isAttacking = false;
    }


	public void Attack() {
        anim.SetInteger("whichWeapon", whichWeapon);
        anim.SetInteger("weapLevel", weapLevel[whichWeapon]);

        if ( !pm.isDash() && !isAttacking) {
            if(!pm.isJump() && !pm.isFall()) 
			    rb.velocity = new Vector2(0, rb.velocity.y);
			if (whichWeapon == 0 && swordDurr <= 0) {
				isAttacking = false;
			} 
			else {
				if (weapLevel [whichWeapon] >= 0) {
					isAttacking = true;
				}
			}

		}
	}
	public void SwitchWeapon() {
		if (!isAttacking) {
            whichWeapon = (whichWeapon + 1) % GAME.num_weapons;
        }
        anim.SetInteger("whichWeapon", whichWeapon);
        anim.SetInteger("weapLevel", weapLevel[whichWeapon]);
        sw.GetComponent<Image>().sprite = weapsp[whichWeapon*GAME.num_weapons]; // + currentlevel
    }

	public void SetAttack(bool a) {
		attackCalled = a;
	}

	public int Damage(){
		return GAME.WEAP_DAMAGE[whichWeapon,weapLevel [whichWeapon]];
	}

    public void CheckAttack() {
        if(!attackCalled && isAttacking) {
            endAttack();
        }
    }

	public void SwordAttackCompleted(){
		swordDurr--;
	}

}
