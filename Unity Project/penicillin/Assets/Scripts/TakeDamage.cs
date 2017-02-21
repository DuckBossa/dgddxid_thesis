using UnityEngine;
using System.Collections;
using GLOBAL;


public class TakeDamage : MonoBehaviour {
	int damage;
	//for projectiles
	public void OnTriggerEnter2D(Collider2D other){
		other.transform.parent.gameObject.GetComponent<IDamage> ().TakeDamage (damage);
		Destroy (other.transform.parent.gameObject);
	}

	public void SetDamage(int dmg){
		damage = damage;
	}
}
