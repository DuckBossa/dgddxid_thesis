using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileDeconstruction : MonoBehaviour {

	BoxCollider2D bounderinos;
	List<Collider2D>tiles;
	void Start(){
		bounderinos = GetComponent<BoxCollider2D> ();
		tiles = new List<Collider2D> ();
	}

	void FixedUpdate(){
		for (int i = tiles.Count - 1; i >= 0; i--) {
			if (bounderinos.bounds.max.y > tiles [i].bounds.max.y) {
				tiles [i].gameObject.SetActive (false);
				tiles.RemoveAt (i);
              
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Equals ("Tiles")) {
			tiles.Add (other);
		}
	}	
}
