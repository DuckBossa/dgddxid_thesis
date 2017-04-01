using UnityEngine;
using UnityEngine.UI;
using GLOBAL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {
    // Use this for initialization
    public Text screenTimer;
    public Slider timeSlider;
    public static float globalTime;
    public Transform player;
    public GameObject c_controls, c_pause, c_hud, dialogues, t_loadoutTrigger, t_loadoutMarker, enemy, rpoints, rlab;
    public Camera minimap;
    public bool checkpoint, researchDone, enemySpawned;

    private string[] messages;
    private bool active, cp1, cp2, cp3, cp4, cp5, cp6, cp7;
    private float timeLimitInSeconds, localTime, levelTime;
    private Vector3 defaultScale;
    private Text text;
    private GameObject pnorm, pangr; //penny normal and angry
    private int cur_msg;
	void Start () {
        timeLimitInSeconds = 60 * GAME.waveTimeInMins;
        levelTime = 60 * GAME.waveTimeInMins * GAME.num_waves;

        //disable everything
        c_controls.SetActive(false);
        c_hud.SetActive(false);
        //c_loadout.SetActive(false);
        c_pause.SetActive(false);
        minimap.enabled = false;
        t_loadoutTrigger.SetActive(false);
        t_loadoutMarker.SetActive(false);

        globalTime = 0;
        timeSlider.value = globalTime / levelTime;
        defaultScale = t_loadoutMarker.transform.localScale;
        screenTimer.text = "";
        active = false;
        cur_msg = 0;

        //tutorial checkpoints
        checkpoint = false;
        researchDone = false;
        cp1 = false;
        cp2 = false;
        cp3 = false;
        cp4 = false;
        cp5 = false;
        cp6 = false;
        cp7 = false;
        enemySpawned = false;

        pnorm = dialogues.transform.GetChild(0).gameObject;
        pangr = dialogues.transform.GetChild(1).gameObject;

        messages = new string[] {
            //0-4
            "Hello there! I'm Private Penny Pennicilin, your trusty antibiotic.",
            "Humans have been abusing antibiotics for a long time now, taking them even when unnecessary, or not finishing their prescribed dosages.",
            "This caused bacteria to evolve and strengthen rapidly to the point where current Antibiotic research cannot keep up.",
            "We need all the help we can get, and I'm glad we have you on our side!",
            "Let's begin with the basics. That's me at the center of the screen. You can control me using the left and right arrows on the bottom left of the screen.",
            //5-9
            "Proceed to the first checkpoint to get started.",
            "", 
            "Great! You can fall off platforms or jump onto higher ones with your jump ability on the bottom right of the screen. Try getting to the next checkpoint.",
            "",
            "That other checkpoint is too far to reach with a single jump. Use your dash ability to leap further.",
            //10-14
            "",
            "You can chain up to three dashes at a time. Just remember that dashing takes some time to regenerate. You are invulnerable for the duration of the dash!",
            "You will have to attack enemy bacteria pretty soon. For that, use the buttons above your jump and dash ability.",
            "Tapping these buttons attack nearby enemies or change the current weapon in-use. The attack button also shows the current weapon's efficacy.",
            "",
            //15-19
            "At the bottom middle of your screen is the minimap. This will show you all of the things currently active on the level, as well as your current location.",
            "Proceed to the next checkpoint. I will mark it for you.",
            "",
            "Did you see that big red circle? That indicates the location of the Research Lab.",
            "The Research Lab is where you can spend Research Points, which you gain from killing bacteria throughout levels.",
            //20-24
            "The lab allows you to upgrade your antibiotics and unlock new ones so you have more options to kill more bacteria.",
            "You can see the amount of research points you currently have at the top of the screen, as well as your current health.",
            "Between the health and research points, you can see a progress bar that indicates how many enemies you need to eliminate to proceed to the next wave.",
            "Pick the pill up and see what the lab has to offer! Here's 200 research points, on me!",
            "",
            //25-29
            "Oh no! A stray shigella appeared out of nowhere!",
            "Good thing you have a weapon now. Quick! Get rid of it!",
            "",
            "That was close! But prepare yourself, as more of them will be invading soon.",
            "To aid you in this task, health pick-ups will randomly appear on the map. Be on the lookout for them!",

            //30-34
            "Remember, you can only purchase and upgrade weapons in the research lab, so make sure you don't miss it!",
            "Enemy bacteria will be more and more resistant to your weapons over time. Make sure you finish as fast as you can.",
            "Researching for new antibiotic weapons make them less resistant, making it easier to clear waves again.",
            "You are now ready to fight the hordes of bacteria infecting our host. Remember what I taught you, and good luck!",
			""
        };

        text = dialogues.transform.GetChild(4).gameObject.GetComponent<Text>();
        text.text = messages[cur_msg];
        ResitanceCalculator.Instance.ResetAll();
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
        if (!cp1 && checkpoint) { //walk towards edge
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp1 = true;
            checkpoint = false;
        }

        if (!cp2 && checkpoint) { //jump to next platform
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp2 = true;
            checkpoint = false;
        }

        if(!cp3 && checkpoint) { // use dash
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp3 = true;
            checkpoint = false;
        }

        if (!cp4 && checkpoint) { //
            player.gameObject.GetComponent<PlayerAttack>().ForceStopAttack();
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp4 = true;
            checkpoint = false;
        }

        if (!cp5 && checkpoint) {
            player.gameObject.GetComponent<PlayerAttack>().ForceStopAttack();
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp5 = true;
            checkpoint = false;
            minimap.enabled = false;
        }

        if (!cp6 && checkpoint) {
            player.gameObject.GetComponent<PlayerAttack>().ForceStopAttack();
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            cp6 = true;
            checkpoint = false;
            minimap.enabled = false;
        }

        if (researchDone) {
            player.gameObject.GetComponent<PlayerAttack>().ForceStopAttack();
            c_controls.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            checkpoint = false;
            minimap.enabled = false;
            researchDone = false;
        }

        if(cur_msg == 27 && enemySpawned) {
            player.gameObject.GetComponent<PlayerAttack>().ForceStopAttack();
            c_controls.SetActive(false);
            c_hud.SetActive(false);
            text.text = messages[++cur_msg];
            dialogues.SetActive(true);
            player.transform.gameObject.GetComponent<PlayerMovement>().hInput = 0;
            minimap.enabled = false;
        }


        if (t_loadoutTrigger.activeInHierarchy) {
            if (t_loadoutMarker.transform.localScale.x >= 0) t_loadoutMarker.transform.localScale -= new Vector3(GAME.loadoutIndicatorDecaySpeed, GAME.loadoutIndicatorDecaySpeed, 0);
        }
    }

    public void NextMessage() {
        // Current text in text area
        try {
            text.text = messages[++cur_msg];
			//Debug.Log(cur_msg);
        }catch(Exception e) {
            throw(e);
        }

        // Penny's current face
        if (cur_msg == 1 || cur_msg == 2 || cur_msg == 12 || cur_msg == 25) {
            pnorm.SetActive(false);
            pangr.SetActive(true);
        }
        else {
            pangr.SetActive(false);
            pnorm.SetActive(true);
        }

        if(cur_msg == 6) { // walk to edge
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(0).gameObject.SetActive(false);
            c_controls.transform.GetChild(1).gameObject.SetActive(false);
            c_controls.transform.GetChild(2).gameObject.SetActive(false);
            c_controls.transform.GetChild(3).gameObject.SetActive(false);
            c_controls.transform.GetChild(4).gameObject.SetActive(false);
        }

        if (cur_msg == 8) { // jump to platform
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (cur_msg == 10) { // use dash
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (cur_msg == 14) { // cp3
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(2).gameObject.SetActive(true);
            c_controls.transform.GetChild(3).gameObject.SetActive(true);
            c_controls.transform.GetChild(4).gameObject.SetActive(true);
        }

        if(cur_msg == 17) {
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            minimap.enabled = true;
            //activate rlab
            t_loadoutTrigger.SetActive(true);
            t_loadoutMarker.SetActive(true);
            t_loadoutMarker.transform.position = t_loadoutTrigger.transform.position;
        }

        if(cur_msg == 24) {// get the pill
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_hud.SetActive(true);
            minimap.enabled = true;
            GetComponent<ResearchPointsManager>().rpcurrent += 200;
            GetComponent<ResearchPointsManager>().rptotal += 200;
            GetComponent<ResearchPointsManager>().rphud.text = GetComponent<ResearchPointsManager>().rpcurrent.ToString("D6");
            GetComponent<ResearchPointsManager>().rplab.text = GetComponent<ResearchPointsManager>().rpcurrent.ToString("D6");
            //rpoints.GetComponent<ScoreManager>().AddPoints(200);
        }

        if(cur_msg == 27) { // spawn bacteria
            dialogues.SetActive(false);
            c_controls.SetActive(true);
            c_controls.transform.GetChild(4).gameObject.SetActive(true);
            c_hud.SetActive(true);
            minimap.enabled = true;
            enemy.SetActive(true);
            //enemySpawned = true;
        }

		if (cur_msg == 34) {
			SceneManager.LoadScene ("Load_MainMenu");
		}
    }

    public void Reset() {
        Time.timeScale = 1;
        timeLimitInSeconds = 60 * GAME.waveTimeInMins;
        localTime = 0;
    }

    public void ResearchDone() {
        researchDone = true;
    }
}
