using UnityEngine;
using System.Collections;

public class FlyingShigellaProjectileHit : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("a");
        other.GetComponent<PlayerHealth>().TakeDamage();
        Destroy(gameObject, 0.1f);
    }
}
