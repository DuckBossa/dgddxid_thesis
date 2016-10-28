using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour {
    public Transform pauseCanvas;
    public Transform audioToggle;
    public Transform quitPrompt;
    public Transform pauseMenu;
    public Transform restartPrompt;
    public Transform mainMenuPrompt;
    bool soundOn = true;
	
	void Start () {
        pauseCanvas.gameObject.SetActive(false);
        quitPrompt.gameObject.SetActive(false);
        restartPrompt.gameObject.SetActive(false);
        mainMenuPrompt.gameObject.SetActive(false);
    }
	
    public void PauseButton() {
        pauseCanvas.gameObject.SetActive(pauseCanvas.gameObject.activeInHierarchy ? false : true);
        Time.timeScale = pauseCanvas.gameObject.activeInHierarchy ? 0 : 1;
    }

    public void ToggleAudio() {
        soundOn = !soundOn;
        audioToggle.GetComponent<Text>().text = soundOn ? "Sound: On" : "Sound: Off";
    }

    public void QuitPrompt() {
        quitPrompt.gameObject.SetActive(quitPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void ExitApplication() {
        Application.Quit();
    }

    public void RestartPrompt() {
        restartPrompt.gameObject.SetActive(restartPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuPrompt() {
        mainMenuPrompt.gameObject.SetActive(mainMenuPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
