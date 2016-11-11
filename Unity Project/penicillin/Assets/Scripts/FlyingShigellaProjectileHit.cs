using UnityEngine;
using System.Collections;

public class FlyingShigellaProjectileHit : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
		other.transform.parent.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        Destroy(gameObject, 0.1f);
    }
}
