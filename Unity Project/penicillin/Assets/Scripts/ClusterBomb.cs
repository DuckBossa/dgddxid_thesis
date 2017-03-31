using UnityEngine;
using System.Collections;
using GLOBAL;

public class ClusterBomb : MonoBehaviour, IPlayerDamage {
	int damage;
	Animator anim;
	bool boomed;
	public void Awake(){
		boomed = false;
		anim = GetComponent<Animator> ();
	}
	//for projectiles

	public void SetDamage(int dmg){
		damage = dmg;
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (!boomed) {
			boom ();	
		}
	}

	public void boom(){
		boomed = false;
		anim.SetTrigger ("boom");
		GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
	}

	public int Damage() {
		return damage;
	}

	public void implode(){
		Destroy (gameObject, 0.2f);
	}

}
