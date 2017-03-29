using UnityEngine;
using System.Collections;

public class ShigellaProjectileHit : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        PlayerHealth ph = other.transform.parent.gameObject.GetComponent<PlayerHealth>();
        if(ph != null) ph.TakeDamage();
        Destroy(gameObject, 0.1f);
    }
}
