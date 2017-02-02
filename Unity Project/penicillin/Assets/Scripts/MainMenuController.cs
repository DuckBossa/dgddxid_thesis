using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Transform t_quit, t_menu, t_tutorial, t_credits;
    public Button b_start, b_quit, b_tutorial, b_credits;
    AudioSource buttonSound;

    void Start() {
        t_quit.gameObject.SetActive(false);
        buttonSound = gameObject.GetComponent<AudioSource>();
    }

    void OnEnable() {
        Time.timeScale = 1;
    }

    public void StartGame() {
        StartCoroutine(Load("LevelStomach"));
    }

    IEnumerator Load(string level) { //need this so the audio plays completely before loading scene
       buttonSound.PlayOneShot(buttonSound.clip);
        yield return new WaitForSeconds(buttonSound.clip.length);
        SceneManager.LoadScene(level);
    }

    IEnumerator QuitGame() {
       buttonSound.PlayOneShot(buttonSound.clip);
        yield return new WaitForSeconds(buttonSound.clip.length);
        Application.Quit();
    }

    public void QuitPrompt() {
        buttonSound.Play();
        toggleUI(t_quit);
    }

    public void Tutorial() {
        StartCoroutine(Load("Tutorial"));
    }

    public void Credits() {
       buttonSound.PlayOneShot(buttonSound.clip);
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
        StartCoroutine(QuitGame());
    }
}
