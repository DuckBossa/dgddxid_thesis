using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GLOBAL;


public class DamageChecks : MonoBehaviour {

    public List<GameObject> listem = new List<GameObject>();

    public void Update() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            foreach (var s in listem) {
				s.transform.parent.gameObject.GetComponent<IDamage>().TakeDamage(1);
            }
        }
    }
}
