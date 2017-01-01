using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Transform t_quit, t_menu, t_tutorial, t_credits;
    public Button b_start, b_quit, b_tutorial, b_credits;
    public AudioClip buttonPress;
    AudioSource audio;
    bool started;

    void Start() {
        t_quit.gameObject.SetActive(false);
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
        toggleUI(t_quit);
    }

    public void Tutorial() {
        audio.Play();
        toggleUI(t_tutorial);
    }

    public void Credits() {
        audio.Play();
        toggleUI(t_credits);
    }

    void toggleUI(Transform obj) {
        obj.gameObject.SetActive(obj.gameObject.activeInHierarchy ? false : true);
        b_start.interactable = obj.gameObject.activeInHierarchy ? false : true;
        b_tutorial.interactable = obj.gameObject.activeInHierarchy ? false : true;
        b_quit.interactable = obj.gameObject.activeInHierarchy ? false : true;
        b_tutorial.interactable = obj.gameObject.activeInHierarchy ? false : true;
    }

    public void ExitApplication() {
        audio.Play();
        Application.Quit();
    }
}
