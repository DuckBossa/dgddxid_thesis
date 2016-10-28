using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Transform quitPrompt;
    public Transform mainMenu;
    public AudioClip buttonPress;
    AudioSource audio;
    bool started;

    void Start() {
        quitPrompt.gameObject.SetActive(false);
        audio = GameObject.Find("ButtonAudio").GetComponent<AudioSource>();
    }

	public void StartGame() {
        audio.Play();
        SceneManager.LoadScene("LevelStomach");
    }

    public void QuitPrompt() {
        audio.Play();
        mainMenu.gameObject.SetActive(mainMenu.gameObject.activeInHierarchy ? false : true);
        quitPrompt.gameObject.SetActive(quitPrompt.gameObject.activeInHierarchy ? false : true);
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
