using UnityEngine;
using System.Collections;
using GLOBAL;
public class GetDamage : MonoBehaviour {

	public float damageTimer = 0.5f;

	float timer;

	public void Start(){
		timer = damageTimer;
	}

	public void Update(){
		if (timer < damageTimer) {
			timer += Time.deltaTime;
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
		
		if (timer >= damageTimer) {
			if (other.gameObject.layer == LayerMask.NameToLayer ("PlayerAttack")) {
				Debug.Log ("keepo");
				Debug.Log (other.gameObject.GetComponent<IPlayerDamage> ().Damage ());
				transform.parent.gameObject.GetComponent<IDamage> ().TakeDamage (other.gameObject.GetComponent<IPlayerDamage> ().Damage ());
				timer = 0;
			}
		}
	}
}
