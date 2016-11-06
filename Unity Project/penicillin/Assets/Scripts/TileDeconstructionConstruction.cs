using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileDeconstructionConstruction : MonoBehaviour {
	public List<GameObject> sets = new List<GameObject>();
	void Start () {}

    public void reconstruct() {
		foreach (GameObject set in sets) {
		    set.SetActive (false);
		}
		int rnglul = Random.Range (0, sets.Count);
		sets [rnglul].SetActive (true);
        foreach (Transform set in sets[rnglul].transform) {
            set.gameObject.SetActive(true);
            set.gameObject.GetComponentInChildren<Transform>().gameObject.SetActive(false);
            BoxCollider2D temp = set.GetComponent<BoxCollider2D>();
            temp.isTrigger = true;
            temp.enabled = true;
        }
        DisableDeconstruction();
	}

    public void EnableDeconstruction() {
        gameObject.GetComponent<TileDeconstruction>().enabled = true;
        gameObject.GetComponent<TileConstruction>().enabled = false;
    }
    public void DisableDeconstruction() {
        gameObject.GetComponent<TileConstruction>().enabled = true;
        gameObject.GetComponent<TileDeconstruction>().enabled = false;
    }
}
