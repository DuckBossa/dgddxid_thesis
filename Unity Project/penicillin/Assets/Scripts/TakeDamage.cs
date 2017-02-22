using UnityEngine;
using System.Collections;
using GLOBAL;


public class TakeDamage : MonoBehaviour,IPlayerDamage {
	int damage;
	//for projectiles
	public void SetDamage(int dmg){
		damage = dmg;
	}

    public void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject, 0.3f);
    }

    public int Damage() {
        return damage;
    }
}
    