using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour {

    public Transform pauseCanvas;
	
	void Start () {
        pauseCanvas.gameObject.SetActive(false);
	}
	
	void Update () {
	    
	}

    public void PauseButton() {
        //if Pause menu is not active, activate; else, deactivate
        pauseCanvas.gameObject.SetActive(pauseCanvas.gameObject.activeInHierarchy ? false : true);
    }
}
