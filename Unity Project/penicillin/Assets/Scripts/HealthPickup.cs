using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

    public GameObject player, mgr, indicator;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Penny") {
            player.GetComponent<PlayerHealth>().GainHealth();
            mgr.GetComponent<StomachLevel_Global>().acidCycleCounter = 0;
            mgr.GetComponent<StomachLevel_Global>().hptimer = 0;
            this.gameObject.SetActive(false);
            indicator.gameObject.SetActive(false);
        }
    }
}
