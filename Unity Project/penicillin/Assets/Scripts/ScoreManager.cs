using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int researchPoints;
    Text text;

    void Start () {
        researchPoints = 0;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Research Points: " + researchPoints;
	}
}
