using UnityEngine;
using UnityEngine.UI;
using GLOBAL;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour {
    // Use this for initialization
    public Text screenTimer;
    public Slider timeSlider;
    public static float globalTime;
    public Transform player;
    public GameObject c_loadout, loadoutIndicator, c_controls, c_pause, c_hud, dialogues;
    public Camera minimap;

    private string[] messages;
    private bool active, cp1, cp2, cp3, wasLoadout;
    private float timeLimitInSeconds, localTime, levelTime;
    private Color defaultColor;
    private Vector3 defaultScale;
    private Text text;
    private GameObject pnorm, pangr;
    private int cur_msg;
	void Start () {
        timeLimitInSeconds = 60 * GAME.waveTimeInMins;
        levelTime = 60 * GAME.waveTimeInMins * GAME.num_waves;

        c_controls.SetActive(false);
        //c_pause.SetActive(false);
        //c_hud.SetActive(false);
        c_loadout.SetActive(false);
        minimap.enabled = false;

        defaultColor = screenTimer.color;
        globalTime = 0;
        loadoutIndicator.SetActive(false);
        timeSlider.value = globalTime / levelTime;
        wasLoadout = false;
        defaultScale = loadoutIndicator.transform.localScale;
        screenTimer.text = "";
        active = false;
        cur_msg = 0;

        //tutorial checkpoints
        cp1 = false;
        cp2 = false;
        cp3 = false;

        pnorm = dialogues.transform.GetChild(0).gameObject;
        pangr = dialogues.transform.GetChild(1).gameObject;

        messages = new string[] {
            "Hello there! I'm Private Penny Pennicilin, your trusty antibiotic.",
            "Humans have been abusing antibiotics for a long time now, taking them even when unnecessary, or not finishing their prescribed dosages.",
            "This caused bacteria to evolve and strengthen rapidly to the point where current Antibiotic research cannot keep up.",
            "We need all the help we can get, and I'm glad we have you on our side!",
            "Let's begin with the basics. That's me at the center of the screen. You can control me using the left and right arrows on the bottom left of the screen",

            "Proceed to the right edge of the platform to get started.",
            "", 
            "Great! You can fall off of platforms or jump onto higher ones with your jump ability on the bottom right of the screen. Try getting to the next platform.",
            "",
            "That other platform is too far for a single jump. Use your dash ability to reach it.",

            "",
            "You can chain dashes up to three times. Just remember that the ability takes some time to regenerate.",
            "You can pause the game anytime by clicking on the pause button on the upper-right corner of the screen. Try accessing the pause menu now.",
            ""
        };

        text = dialogues.transform.GetChild(4).gameObject.GetComponent<Text>();
        text.text = messages[cur_msg];
    }
	
    void OnEnable() {
        Time.timeScale = 1;
    }

    void FixedUpdate() {
        if(active) timeSlider.value = globalTime / levelTime;
    }

    void Update() {
        globalTime += Time.deltaTime; //time in seconds
        localTime += Time.deltaTime; //time used to calculate the time to display on screen; separate from globalTime; should not be used for anything else 
        int timeRemaining = (int)timeLimitInSeconds - (int)localTime;

        //triggers
        if (!cp1 && player.position.x > -8.75f) { //walk towards edge
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp1 = true;
        }

        if (!cp2 && player.position.x > 0 && player.position.y > 1) { //jump to next platform
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp2 = true;
        }

        if(!cp3 &&player.position.x > 5 && player.position.y > 1) { // use dash
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp3 = true;
        }

        /*
        //spawn loadout section if it's not yet there
        if (timeRemaining == -1 && !c_loadout.activeInHierarchy) {
            //enable c_loadout gameobject
            c_loadout.transform.position = loadouts[(int)Random.Range(0, loadouts.Length)].transform.position;
            c_loadout.SetActive(true);
            screenTimer.color = Color.red;
            localTime = 0;
            timeLimitInSeconds = GAME.loadoutLifetime;
            wasLoadout = true;
        }
        //player didn't reach loadoutsection in time, go straight to 2nd wave
        else if (timeRemaining == -1 && c_loadout.activeInHierarchy) {
            screenTimer.text = "";
            localTime = 0;
            timeLimitInSeconds = 60 * GAME.waveTimeInMins;
            screenTimer.color = defaultColor;
            c_loadout.SetActive(false);
        }

        if (c_loadout.activeInHierarchy) {
            loadoutIndicator.SetActive(true);
            loadoutIndicator.transform.position = c_loadout.transform.position;
            if (loadoutIndicator.transform.localScale.x >= 0) loadoutIndicator.transform.localScale -= new Vector3(GAME.loadoutIndicatorDecaySpeed, GAME.loadoutIndicatorDecaySpeed, 0);
            string min = Mathf.Floor(timeRemaining / 60).ToString("00");
            string sec = (timeRemaining % 60).ToString("00");
            screenTimer.text = min + ":" + sec;
        }
        else if (!c_loadout.activeInHierarchy && wasLoadout) {
            screenTimer.text = "";
            loadoutIndicator.SetActive(false);
            loadoutIndicator.transform.localScale = defaultScale;
        }
        */
    }

    public void NextMessage() {
        // Current text in text area
        try {
            text.text = messages[++cur_msg];
        }catch(Exception exc) {
            throw;
        }

        // Penny's current face
        if (cur_msg == 1 || cur_msg == 2) {
            pnorm.SetActive(false);
            pangr.SetActive(true);
        }
        else {
            pangr.SetActive(false);
            pnorm.SetActive(true);
        }

        if(cur_msg == 6) {
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(0).gameObject.SetActive(false);
            c_controls.transform.GetChild(1).gameObject.SetActive(false);
            c_controls.transform.GetChild(2).gameObject.SetActive(false);
            c_controls.transform.GetChild(3).gameObject.SetActive(false);
            c_controls.transform.GetChild(4).gameObject.SetActive(false);
        }

        if (cur_msg == 8) { // cp1
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(0).gameObject.SetActive(true);
            //c_controls.transform.GetChild(1).gameObject.SetActive(false);
            //c_controls.transform.GetChild(2).gameObject.SetActive(false);
            //c_controls.transform.GetChild(3).gameObject.SetActive(false);
            //c_controls.transform.GetChild(4).gameObject.SetActive(false);
        }

        if (cur_msg == 10) { // cp2
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(0).gameObject.SetActive(true);
            c_controls.transform.GetChild(1).gameObject.SetActive(true);
            //c_controls.transform.GetChild(2).gameObject.SetActive(false);
            //c_controls.transform.GetChild(3).gameObject.SetActive(false);
            //c_controls.transform.GetChild(4).gameObject.SetActive(false);
        }

        if (cur_msg == 13) { // cp3
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            //c_controls.transform.GetChild(0).gameObject.SetActive(true);
            //c_controls.transform.GetChild(1).gameObject.SetActive(true);
            //c_controls.transform.GetChild(2).gameObject.SetActive(false);
            //c_controls.transform.GetChild(3).gameObject.SetActive(false);
            c_controls.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void Reset() {
        Time.timeScale = 1;
        timeLimitInSeconds = 60 * GAME.waveTimeInMins;
        localTime = 0;
        c_loadout.SetActive(false);
        screenTimer.color = defaultColor;
    }
}
