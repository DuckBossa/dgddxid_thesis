using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
        
	
    public void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage();
    }

}
