using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Transform quitPrompt;
    public Transform mainMenu;
    public AudioClip buttonPress;
    public Button start, tutorial, quitgame;
    AudioSource audio;
    bool started;

    void Start() {
        quitPrompt.gameObject.SetActive(false);
        audio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
    }

    void OnEnable() {
        Time.timeScale = 1;
    }

	public void StartGame() {
        audio.Play();
        SceneManager.LoadScene("LevelStomach");
    }

    public void QuitPrompt() {
        audio.Play();
        quitPrompt.gameObject.SetActive(quitPrompt.gameObject.activeInHierarchy ? false : true);
        start.interactable = quitPrompt.gameObject.activeInHierarchy ? false : true;
        tutorial.interactable = quitPrompt.gameObject.activeInHierarchy ? false : true;
        quitgame.interactable = quitPrompt.gameObject.activeInHierarchy ? false : true;
    }

    public void Tutorial() {
        audio.Play();
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitApplication() {
        audio.Play();
        Application.Quit();
    }
}
