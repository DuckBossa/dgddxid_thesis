using UnityEngine;
using UnityEngine.UI;
using GLOBAL;
using System.Collections;

public class StomachLevel_Global : MonoBehaviour {
    // Use this for initialization
    public Text screenTimer;
    public Slider timeSlider;
    public static float globalTime;
    public Transform[] loadouts;
    public Transform player;
    public GameObject Loadout, loadoutIndicator;

    private float timeLimitInSeconds, localTime, levelTime;
    private bool wasLoadout;
    private Color defaultColor;
    private Vector3 defaultScale;
	void Start () {
        Time.timeScale = 1;
        //timeLimitInSeconds = 60*GAME.waveTimeInMins;
        timeLimitInSeconds = 1;
        defaultColor = screenTimer.color;
        Loadout.SetActive(false);
        loadoutIndicator.SetActive(false);
        levelTime = 60 * GAME.waveTimeInMins * GAME.num_waves;
        timeSlider.value = globalTime / levelTime;
        wasLoadout = false;
        defaultScale = loadoutIndicator.transform.localScale;
        screenTimer.text = "";
    }
	
	// Update is called once per frame
    void FixedUpdate() {
        timeSlider.value = globalTime / levelTime;
    }

	void Update () {
        globalTime += Time.deltaTime; //time in seconds
        localTime += Time.deltaTime; //time used to calculate the time to display on screen; separate from globalTime; should not be used for anything else 

        int timeRemaining = (int)timeLimitInSeconds - (int)localTime;
        

        //spawn loadout section if it's not yet there
        if(timeRemaining == -1 && !Loadout.activeInHierarchy) {
            //retrieve furthest
            float maxDist = Mathf.NegativeInfinity;
            int furthest = -1;
            for(int i = 0; i < loadouts.Length; i++) {
                float tempdist = (loadouts[i].transform.position - player.transform.position).sqrMagnitude;
                if ( tempdist > maxDist) {
                    maxDist = tempdist;
                    furthest = i;
                }
            }
            //enable loadout gameobject
            Loadout.transform.position = loadouts[furthest].transform.position;
            Loadout.SetActive(true);
            screenTimer.color = Color.red;
            localTime = 0;
            timeLimitInSeconds = GAME.loadoutLifetime;
            wasLoadout = true;
        }
        //player didn't reach loadoutsection in time, go straight to 2nd wave
        else if(timeRemaining == -1 && Loadout.activeInHierarchy) {
            screenTimer.text = "";
            localTime = 0;
            timeLimitInSeconds = 60 * GAME.waveTimeInMins;
            screenTimer.color = defaultColor;
            Loadout.SetActive(false);
        }

        if (Loadout.activeInHierarchy) {
            loadoutIndicator.SetActive(true);
            loadoutIndicator.transform.position = Loadout.transform.position;
            if(loadoutIndicator.transform.localScale.x >= 0) loadoutIndicator.transform.localScale -= new Vector3(GAME.loadoutIndicatorDecaySpeed, GAME.loadoutIndicatorDecaySpeed, 0);
            string min = Mathf.Floor(timeRemaining / 60).ToString("00");
            string sec = (timeRemaining % 60).ToString("00");
            screenTimer.text = min + ":" + sec;
        }
        else if(!Loadout.activeInHierarchy && wasLoadout) {
            screenTimer.text = "";
            loadoutIndicator.SetActive(false);
            loadoutIndicator.transform.localScale = defaultScale;
        }
	}

    public void Reset() {
        Time.timeScale = 1;
        timeLimitInSeconds = 60 * GAME.waveTimeInMins;
        localTime = 0;
        Loadout.SetActive(false);
        screenTimer.color = defaultColor;
    }
}
