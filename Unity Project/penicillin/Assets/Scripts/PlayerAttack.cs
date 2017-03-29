using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using GLOBAL;

public class PlayerAttack : MonoBehaviour,IPlayerDamage {
    int whichWeapon;
	int[] weapLevel = new int[GAME.num_weapons];
    bool isAttacking,attackCalled;
    bool hasAmmo;
    Animator anim;
	PlayerMovement pm;
	Rigidbody2D rb;
    public Sprite[] weapsp;
    public Image resistanceDisplay;
    public GameObject atkb; /* atk button */


    void Awake() {
		pm = GetComponent<PlayerMovement> ();
		rb = GetComponent<Rigidbody2D> ();
        hasAmmo = true;
		attackCalled = false;
		whichWeapon = 0;
		for (int i = 0; i < weapLevel.Length; i++) {
            weapLevel[i] = -1;//-1;
		}
		anim = GetComponent<Animator>();
        isAttacking = false;
    }

    void Start() {
        SwitchWeapon();
    }

    void FixedUpdate() {
		if (attackCalled && !pm.isDash ())
			Attack ();
        anim.SetBool("isAttacking", isAttacking);
        if (weapLevel[whichWeapon] != -1) {
            if(ResitanceCalculator.Instance != null)
                resistanceDisplay.fillAmount = 1.0f - (ResitanceCalculator.Instance.GetResistanceModifier(whichWeapon) / GAME.peakResist);
        }
        else resistanceDisplay.fillAmount = 0;
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
        anim.SetInteger("whichWeapon", whichWeapon);
        anim.SetInteger("weapLevel", weapLevel[whichWeapon]);
    }

    void endAttack() {
        isAttacking = false;
    }


	public void Attack() {
        if(weapLevel[whichWeapon] != -1) {
            anim.SetInteger("whichWeapon", whichWeapon);
            anim.SetInteger("weapLevel", weapLevel[whichWeapon]);

            if (!pm.isDash() && !isAttacking) {
                if (!pm.isJump() && !pm.isFall())
                    rb.velocity = new Vector2(0, rb.velocity.y);
                isAttacking = true;
            }
        }
	}
	public void SwitchWeapon() {
		if (!isAttacking) {
            do {
                whichWeapon = (whichWeapon + 1) % GAME.num_weapons;
            } while (weapLevel[whichWeapon] == -1);

            anim.SetInteger("whichWeapon", whichWeapon);
            anim.SetInteger("weapLevel", weapLevel[whichWeapon]);
            atkb.GetComponent<Image>().sprite = weapsp[whichWeapon * GAME.num_weapons + weapLevel[whichWeapon]];
        }
    }

	public void SetAttack(bool a) {
		attackCalled = a;
	}

	public int Damage(){
        return Mathf.CeilToInt((GAME.WEAP_DAMAGE[0, GetWeapLevel(0)] * (1 /*can be modified to be above???*/ - ResitanceCalculator.Instance.GetResistanceModifier(0))));
	}

    public void CheckAttack() {
        if(!attackCalled && isAttacking) {
            endAttack();
        }
    }

	public void SwordAttackCompleted(){
        CheckAttack();
	}

}
