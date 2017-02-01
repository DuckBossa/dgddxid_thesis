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
    bool soundOn = true;

	void Start () {
        buttonSound = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        pauseCanvas.gameObject.SetActive(false);
        quitPrompt.gameObject.SetActive(false);
        restartPrompt.gameObject.SetActive(false);
        mainMenuPrompt.gameObject.SetActive(false);
    }

    public void PauseButton() {
        buttonSound.Play();
        pauseCanvas.gameObject.SetActive(pauseCanvas.gameObject.activeInHierarchy ? false : true);
        Time.timeScale = pauseCanvas.gameObject.activeInHierarchy ? 0 : 1;
    }

    public void ToggleAudio() {
        buttonSound.Play();
        soundOn = !soundOn;
        audioToggle.GetComponent<Text>().text = soundOn ? "Sound: On" : "Sound: Off";
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
