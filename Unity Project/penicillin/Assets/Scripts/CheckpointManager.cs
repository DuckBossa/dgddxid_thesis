using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

    public GameObject manager;
    public GameObject[] checkpointPositions;
    int current = 0;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "BodyCollider") {
            manager.GetComponent<Tutorial>().checkpoint = true;
            gameObject.transform.position = checkpointPositions[current++].transform.position;
        }
    }
}
