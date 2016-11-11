using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StomachLevel_Global : MonoBehaviour {
    public float globalTime;
    // Use this for initialization
    public Text screenTimer;
    public float timeLimitInMinutes;
	void Start () {
        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
        globalTime += Time.deltaTime; //time in seconds
	}
}
