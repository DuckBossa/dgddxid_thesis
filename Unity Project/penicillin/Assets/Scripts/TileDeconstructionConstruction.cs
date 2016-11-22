using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileDeconstructionConstruction : MonoBehaviour {
	public List<GameObject> sets = new List<GameObject>();
    public Animator anim_back;
    int rnglul;
    Animator anim;
    
    bool stop;
	void Start () {
        anim = GetComponent<Animator>();
        stop = false;
		rnglul = Random.Range (0, sets.Count);
	}

    public void BossBattle() {
        stop = true;
    }

    public void reconstruct() {
		foreach (GameObject set in sets) {
		    set.SetActive (false);
		}
		rnglul = Random.Range (0, sets.Count);
		sets [rnglul].SetActive (true); 
        foreach (Transform set in sets[rnglul].transform) {
			set.gameObject.SetActive(true);
        }
		DisableDeconstruction();
	}

    public void EnableDeconstruction() {
        if (!stop) {
            gameObject.GetComponent<TileDeconstruction>().enabled = true;
            gameObject.GetComponent<TileConstruction>().enabled = false;
        }
        else {
            anim.SetTrigger("Boss");
            anim_back.SetTrigger("Boss");
            Debug.Log("LOL");
        }

    }
    public void DisableDeconstruction() {
        gameObject.GetComponent<TileConstruction>().enabled = true;
		gameObject.GetComponent<TileConstruction> ().setColliders (sets [rnglul].GetComponentsInChildren<BoxCollider2D>());
        gameObject.GetComponent<TileDeconstruction>().enabled = false;
    }
}
