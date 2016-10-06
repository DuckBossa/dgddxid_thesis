using UnityEngine;
using System.Collections;

public class ShigellaFlyingRadar : MonoBehaviour {


    public FlyingShigella fs;

    void OnTriggerEnter2D(Collider2D other) {
        fs.setAttackDir((other.transform.position - transform.position).normalized);
        fs.isAttack(true);
    }

    void OnTriggerStay2D(Collider2D other) {
        fs.setAttackDir((other.transform.position - transform.position).normalized);
    }

    void OnTriggerExit2D(Collider2D other) {
        fs.isAttack(false);
    }

}
