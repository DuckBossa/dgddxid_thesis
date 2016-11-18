using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GLOBAL;

public class GameOverScreenHandler : MonoBehaviour {

    public static Text timePlayed, researchPoints;
    public Text tp, rp;

    public void RestartLevel() {
        SceneManager.LoadScene("LevelStomach");// TEMPORARY ONLY; We somehow have to pass what level to restart
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    void Start() {
        gameObject.SetActive(false);
        timePlayed = tp;
        researchPoints = rp;
    }

    public static void displayStats() {
        float totalTime = StomachLevel_Global.globalTime;
        string min = Mathf.Floor(totalTime / 60).ToString("00");
        string sec = (totalTime % 60).ToString("00");
        timePlayed.text = "TIME PLAYED: " + min + ":" + sec;

        researchPoints.text = "TOTAL RESEARCH POINTS: " + ScoreManager.totalResearchPoints;
    }
}
