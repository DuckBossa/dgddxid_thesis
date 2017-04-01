using UnityEngine;
using System.Collections;

public class RlabDisabler : MonoBehaviour {

    // Use this for initialization
    public GameObject rlab;
    int count = 0;
	void Start () {
	    
	}

    public void AddCount() {
        count++;
    }
	// Update is called once per frame
	void Update () {
	    if(count == 3) {
            rlab.SetActive(false);
            gameObject.SetActive(false);
        }
	}
}
