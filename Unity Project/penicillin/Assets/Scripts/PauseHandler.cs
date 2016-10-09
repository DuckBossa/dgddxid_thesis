using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour {

    public Transform pauseCanvas;
    public Transform audioToggle;
    bool soundOn = true;
	
	void Start () {
        pauseCanvas.gameObject.SetActive(false);
	}
	
	void Update () {
	    
	}

    public void PauseButton() {
        //if Pause menu is not active, activate; else, deactivate
        pauseCanvas.gameObject.SetActive(pauseCanvas.gameObject.activeInHierarchy ? false : true);
        //if Pause menu is active, set time to 0; else, set to 1
        Time.timeScale = pauseCanvas.gameObject.activeInHierarchy ? 0 : 1;
    }

    public void ToggleAudio() {
        soundOn = !soundOn;
        audioToggle.GetComponent<Text>().text = soundOn ? "Sound: On" : "Sound: Off";
    }

    public void RestartLevel() {

    }
}
