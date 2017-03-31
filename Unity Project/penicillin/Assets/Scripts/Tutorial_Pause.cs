using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_Pause : MonoBehaviour {
    public Transform pauseCanvas, mainMenuPrompt, restartPrompt, pauseMenu, audioToggle, quitPrompt;
    public Camera gimmeaudio;
    AudioSource buttonSound;
    bool soundOn = true;

	void Start () {
        quitPrompt.gameObject.SetActive(false);
        restartPrompt.gameObject.SetActive(false);
        mainMenuPrompt.gameObject.SetActive(false);
        buttonSound = gimmeaudio.GetComponent<AudioSource>();
    }

    public void PauseButton() {
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Load_Tutorial");
    }

    public void MainMenuPrompt() {
        buttonSound.Play();
        mainMenuPrompt.gameObject.SetActive(mainMenuPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("Load_MainMenu");
    }
}
