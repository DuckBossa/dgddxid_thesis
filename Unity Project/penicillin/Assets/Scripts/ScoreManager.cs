using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int researchPoints;
    public static int totalResearchPoints;
    public Text text;

    void Start () {
        researchPoints = 0;
        totalResearchPoints = 0;
	}
	
	void Update () {
        text.text = researchPoints.ToString("D8");
    }
}
