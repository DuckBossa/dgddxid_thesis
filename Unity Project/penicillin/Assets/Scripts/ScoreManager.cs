using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GLOBAL;

public class ScoreManager : MonoBehaviour {

    public static int researchPoints;
    public static int totalResearchPoints;
    public Text text;

    void Start () {
		researchPoints = PlayerPrefs.GetInt (GAME.PLAYER_PREFS_RP, 0);
        totalResearchPoints = 0;
	}
	
	void Update () {
        text.text = researchPoints.ToString("D8");
    }


	public void SaveData(){
		PlayerPrefs.SetInt (GAME.PLAYER_PREFS_RP, researchPoints);
	}

}
