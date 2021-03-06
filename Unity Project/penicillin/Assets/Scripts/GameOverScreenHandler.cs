﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GLOBAL;

public class GameOverScreenHandler : MonoBehaviour {

    public static Text timePlayed, researchPoints;
    public Text tp, rp;
    public GameObject score;

    //private ScoreManager smgr;

    public void RestartLevel() {
        SceneManager.LoadScene("LevelStomach");// TEMPORARY ONLY; We somehow have to pass what level to restart
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    void Start() {
        //smgr = score.GetComponent<ScoreManager>();
        gameObject.SetActive(false);
        timePlayed = tp;
        researchPoints = rp;
    }

    public void displayStats() {
        float totalTime = StomachLevel_Global.globalTime;
        string min = Mathf.Floor(totalTime / 60).ToString("00");
        string sec = (totalTime % 60).ToString("00");
        timePlayed.text = min + ":" + sec;
        //researchPoints.text = "" + smgr.totalResearchPoints;
    }
}
