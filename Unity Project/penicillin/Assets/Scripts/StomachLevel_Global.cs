using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StomachLevel_Global : MonoBehaviour {
    public float globalTime;
    // Use this for initialization
    public Text screenTimer;
    public float timeLimitInMinutes;
    private float timeLimitInSeconds;
	void Start () {
        Time.timeScale = 1;
        timeLimitInSeconds = 60*timeLimitInMinutes;
	}
	
	// Update is called once per frame
	void Update () {
        globalTime += Time.deltaTime; //time in seconds
        float timeRemaining = timeLimitInSeconds - globalTime;
        string min = Mathf.Floor(timeRemaining / 60).ToString("00");
        string sec = (timeRemaining % 60).ToString("00");
        screenTimer.text = min + ":" + sec;

        if(timeLimitInSeconds == 0) {
            //trigger an event
        }
	}
}
