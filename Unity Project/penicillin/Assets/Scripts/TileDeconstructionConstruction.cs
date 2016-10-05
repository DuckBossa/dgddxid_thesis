using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileDeconstructionConstruction : MonoBehaviour {
	public List<GameObject> sets = new List<GameObject>();
	void Start () {
		reconstruct ();
	}

    public void reconstruct() {
		foreach (GameObject set in sets) {
			set.SetActive (false);
		}
		int rnglul = Random.Range (0, sets.Count);
		sets [rnglul].SetActive (true);

	}
}
