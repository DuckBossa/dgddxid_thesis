using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverScreenHandler : MonoBehaviour {

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelStomach");// TEMPORARY ONLY; We somehow have to pass what level to restart
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
