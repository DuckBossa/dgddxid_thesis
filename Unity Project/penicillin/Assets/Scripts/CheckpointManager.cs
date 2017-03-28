using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

    public GameObject manager, cpind, player, cparr;
    public GameObject[] checkpointPositions;
    int current = 0;
    float factor, bet, minDist;
    // Use this for initialization
    void Start () {
        factor = .5f; // multiplier of normalized direction vector; offset of objects to penny
        bet = .3f; // dist between icon and arrow
        minDist = 1.3f; // dist between icons to checkpoint before they disappear
	}
	
	// Update is called once per frame
	void Update () {
        // get vectors
        Vector3 cppos = this.transform.position;
        Vector3 plpos = player.transform.position;
        Vector3 directionVector = new Vector3(cppos.x - plpos.x, cppos.y - plpos.y);
        float length = directionVector.magnitude; // length of the vector from penny to the checkpoint
        Vector3 normvec = directionVector / length; //normalized vector from penny to checkpoint

        // set the position of the marker in front of penny
        if (length > minDist) {
            cpind.transform.position = new Vector3(plpos.x + normvec.x * factor, plpos.y + normvec.y * factor); // move a certain length from the player's position
            cparr.transform.position = new Vector3(plpos.x + normvec.x * (factor + bet), plpos.y + normvec.y * (factor + bet)); // set the arrow pos
            if (!cpind.activeInHierarchy) {
                cpind.SetActive(true);
                cparr.SetActive(true);
            }
        }
        else {
            cpind.SetActive(false);
            cparr.SetActive(false);
        }

        // set the rotation of the arrow indicator
        float rad = Mathf.Atan2(directionVector.y, directionVector.x);
        cparr.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg*rad);
	}

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "BodyCollider") {
            manager.GetComponent<Tutorial>().checkpoint = true;

            if (current + 1 < 5) gameObject.transform.position = checkpointPositions[current++].transform.position;
            else gameObject.SetActive(false);
        }
    }
}
