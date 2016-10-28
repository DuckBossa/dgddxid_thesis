using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int researchPoints;
    public Text text;

    void Start () {
        researchPoints = 0;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = researchPoints.ToString("D8");
        //text.text = ""+researchPoints;
    }
}
