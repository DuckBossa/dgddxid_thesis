using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Transform quitPrompt;
    public Transform mainMenu;

    void Start() {
        quitPrompt.gameObject.SetActive(false);
    }

	public void StartGame() {
        SceneManager.LoadScene("LevelStomach");
    }

    public void QuitPrompt() {
        mainMenu.gameObject.SetActive(mainMenu.gameObject.activeInHierarchy ? false : true);
        quitPrompt.gameObject.SetActive(quitPrompt.gameObject.activeInHierarchy ? false : true);
    }

    public void ExitApplication() {
        Application.Quit();
    }
}
