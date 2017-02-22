using UnityEngine;
using System.Collections;

public class ShigellaFlyingRadar : MonoBehaviour {


    public FlyingShigella fs;

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
			fs.setAttackDir((other.transform.position - transform.position).normalized);
			fs.isAttack(true);
		}

    }

    void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
			fs.setAttackDir((other.transform.position - transform.position).normalized);
			fs.isAttack(true);
		}
    }

    void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
			//fs.setAttackDir((other.transform.position - transform.position).normalized);
			fs.isAttack(false);
		}
    }

}
