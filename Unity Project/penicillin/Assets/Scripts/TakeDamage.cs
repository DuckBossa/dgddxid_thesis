using UnityEngine;
using System.Collections;
using GLOBAL;


public class TakeDamage : MonoBehaviour,IPlayerDamage {
	int damage;
	//for projectiles
	public void SetDamage(int dmg){
		damage = dmg;
	}

    public int Damage() {
        return damage;
    }
}
    