using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileConstruction : MonoBehaviour {

    BoxCollider2D bounderinos;
    List<Collider2D> tiles;
    void Start() {
        bounderinos = GetComponent<BoxCollider2D>();
        tiles = new List<Collider2D>();
    }

    void FixedUpdate() {
        for (int i = tiles.Count - 1; i >= 0; i--) {
            if (bounderinos.bounds.min.y <  tiles[i].bounds.min.y) {
                tiles[i].gameObject.GetComponentInChildren<Transform>().gameObject.SetActive(true);
                tiles[i].gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                tiles.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Tiles")) {
            tiles.Add(other);
        }
    }
}
