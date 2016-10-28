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
    public AudioClip buttonPress;
    AudioSource audio;
    bool soundOn = true;
    bool coroutineCalled;

	void Start () {
        audio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
        //Debug.Log(GameObject.Find("ButtonAudio").name);
        pauseCanvas.gameObject.SetActive(false);
        quitPrompt.gameObject.SetActive(false);
        restartPrompt.gameObject.SetActive(false);
        mainMenuPrompt.gameObject.SetActive(false);
    }

    public void PauseButton() {
        audio.Play();
        pauseCanvas.gameObject.SetActive(pauseCanvas.gameObject.activeInHierarchy ? false : true);
        Time.timeScale = pauseCanvas.gameObject.activeInHierarchy ? 0 : 1;
    }

    public void ToggleAudio() {
        audio.Play();
        soundOn = !soundOn;
        audioToggle.GetComponent<Text>().text = soundOn ? "Sound: On" : "Sound: Off";
    }

    public void QuitPrompt() {
        audio.Play();
        quitPrompt.gameObject.SetActive(quitPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void ExitApplication() {
        Application.Quit();
    }

    public void RestartPrompt() {
        audio.Play();
        restartPrompt.gameObject.SetActive(restartPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void RestartLevel() {
        audio.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuPrompt() {
        audio.Play();
        mainMenuPrompt.gameObject.SetActive(mainMenuPrompt.gameObject.activeInHierarchy ? false : true);
        pauseMenu.gameObject.SetActive(pauseMenu.gameObject.activeInHierarchy ? false : true);
    }

    public void GoToMainMenu() {
        audio.Play();
        SceneManager.LoadScene("MainMenu");
    }
}
