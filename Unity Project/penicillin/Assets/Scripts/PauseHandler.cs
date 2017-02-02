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
    AudioSource buttonSound;
    public Camera mainCam;
    bool soundOn = true;

	void Start () {
        buttonSound = gameObject.GetComponent<AudioSource>();
        pauseCanvas.gameObject.SetActive(false);
        quitPrompt.gameObject.SetActive(false);
        restartPrompt.gameObject.SetActive(false);
        mainMenuPrompt.gameObject.SetActive(false);
    }

    public void PauseButton() {
        //order matters; can't play audio from a disabled object
        if (pauseCanvas.gameObject.activeInHierarchy) {
            AudioSource.PlayClipAtPoint(buttonSound.clip, mainCam.transform.position);
            pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else {
            pauseCanvas.gameObject.SetActive(true);
            AudioSource.PlayClipAtPoint(buttonSound.clip, mainCam.transform.position);
            Time.timeScale = 0;
        }
    }

    public void ToggleAudio() {
        soundOn = !soundOn;
        audioToggle.GetComponent<Text>().text = soundOn ? "Sound: On" : "Sound: Off";
        buttonSound.Play();
    }

    public void QuitPrompt() {
        buttonSound.Play();
        quitPrompt.gameObject.SetActive(quitPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void ExitApplication() {
        Application.Quit();
    }

    public void RestartPrompt() {
        buttonSound.Play();
        restartPrompt.gameObject.SetActive(restartPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void RestartLevel() {
        buttonSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuPrompt() {
        buttonSound.Play();
        mainMenuPrompt.gameObject.SetActive(mainMenuPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void GoToMainMenu() {
        buttonSound.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
