using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileConstruction : MonoBehaviour {

    BoxCollider2D bounderinos;
	List<BoxCollider2D> tiles= new List<BoxCollider2D>();
	List<Transform> go_tiles = new List<Transform>();
    public GameObject mgr;
	bool check;
    void Start() {
        bounderinos = GetComponent<BoxCollider2D>();
        
    }

	void OnEnable(){
		tiles.Clear ();
		go_tiles.Clear ();
        //Debug.Log(++mgr.GetComponent<StomachLevel_Global>().acidCycleCounter);
        mgr.GetComponent<StomachLevel_Global>().acidCycleCounter++;
	}
	void OnDisable(){
		//tiles.Clear ();
		//go_tiles.Clear ();
	}

    void FixedUpdate() {
        for (int i = tiles.Count - 1; i >= 0; i--) {
			if (bounderinos.bounds.max.y <  tiles[i].bounds.min.y) {
				tiles [i].gameObject.SetActive (true);
                tiles.RemoveAt(i);
            }
        }
    }
	public void setColliders (BoxCollider2D[] go){
		for (int i = 0; i < go.Length; i++) {
			tiles.Add(go[i]);
			go [i].gameObject.SetActive (false);
		}
	}
}
