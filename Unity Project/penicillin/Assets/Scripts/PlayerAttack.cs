using UnityEngine;
using System.Collections;
using System;
using GLOBAL;

public class PlayerAttack : MonoBehaviour {
    int weapon_switch;
    int weapon_level;
    bool isAttacking;
    Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
        isAttacking = false;
    }

    public void OnTriggerEnter2D(Collider2D other) {
       other.gameObject.GetComponent<EnemyHealth>().TakeDamage();
    }

    void FixedUpdate() {
        anim.SetBool("isAttacking", isAttacking);
    }

    void LateUpdate() {
        if (isAttacking) {
            var subSprites = Resources.LoadAll<Sprite>(GAME.character_weapons_folder + GAME.character_weapon_swords[weapon_switch]);
            foreach (var renderer in GetComponentsInChildren<SpriteRenderer>()) {
                string spriteName = renderer.sprite.name;
                var newSprite = Array.Find(subSprites, item => item.name == spriteName);
                if (newSprite)
                    renderer.sprite = newSprite;
            }
        }

    }

    bool isAttack() {
        return isAttacking;
    }

    void endAttack() {
        isAttacking = false;
    }

}
