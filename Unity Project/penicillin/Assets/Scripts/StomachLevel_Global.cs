using UnityEngine;
using UnityEngine.UI;
using GLOBAL;
using System;
using System.Collections;

public class StomachLevel_Global : MonoBehaviour {
    /*
         To-do:
         
    */

    public int kills;
    public Text screenTimer, dialogueTextArea;
    public Slider enemyCountSlider;
    public static float globalTime;
    public Transform[] loadouts;
    public Transform[] health;
    public Transform player;
    public GameObject pill,
        loadoutIndicator,
        Shigellang_Dormant,
        healthPickup, healthPickupIndicator,
        dialogues,
        c_controls,
        c_hud;
    public GameObject[] spawnWaveLVLS;
    public GameObject bossSpawnDefense, angry, normal;
    public int acidCycleCounter;
    bool bossFight;
    bool bossDormant;
    float pillTimer;

    private float waveTimeInSeconds, waveTime, levelTime, hplifetime, plifetime;
    private bool waspill, freeze, w1;
    private Color defaultColor;
    private Vector3 defaultScale;
    private int waveCounter, cur_msg;
    private string[] msgs;

    void Start() {
        enemyCountSlider.value = 0;
        waveCounter = 1;
        spawnWaveLVLS[waveCounter - 1].SetActive(true);
        for(int i = waveCounter + 1; i <= GAME.LevelStomach_WAVECOUNTER; i++) {
            spawnWaveLVLS[i - 1].SetActive(false);
        }
        enemyCountSlider.maxValue = GAME.NUM_BACTERIA_WAVE[waveCounter - 1];

        pill.SetActive(false);
        loadoutIndicator.SetActive(false);
        waspill = false; /* Did the pill spawn? */
        defaultScale = loadoutIndicator.transform.localScale; /* Editing the scale through script instead of animator */
        screenTimer.text = "";
        bossDormant = true;
        acidCycleCounter = 0; /* Using this counter to determine when to spawn health pickups */
        waveTime = 0; /* Time elapsed for each wave (resets every next wave) */
        hplifetime = 0; /* Lifetime of health pickup */
        plifetime = 0; /* Lifetime of pill */
        //kills = 0;
        kills = 19;
        freeze = false;
        w1 = false;

        /* For Dialogue */
        cur_msg = -1;
        
        msgs = new string[] {
            //introductory message
            "Our host's stomach has been infected by Shigella bacteria. Get rid of them before they pose a threat to our host's health!",
            "Be careful of the green acid! If you fall into the pit, you'll get damaged!",
            "To progress through the level, eliminate enough enemies as quick as you can. Keep an eye on your progress bar, as well as health pickups along the way.",
            "I know you can do this. Good luck!",
            "",

            //after finishing wave 1
            "Great work, Private!",
            "You managed to clear out the first wave of bacteria that's harming our host.",
            "The research lab is going to appear soon, as it's time for your host to take another dose of antibiotics. Don't miss it! Make sure you upgrade your weapons wisely!",
            "",

            //after finishing wave 3
            "Oh no... Is that what I think it is?",
            "It's Shigellang: The Indifferent!",
            "You have to help me stop him before he takes over our host! Quick, break its outer shell and defeat it once and for all!",
            "",

            //if success, cur_msg = 13
            "You did it! You stopped the Shigella invasion by properly using antibiotics!",
            "Our fight does not end here. More and more bacteria evolve as time passes, and many people still need to be educated on antibiotic misuse and abuse.",
            "I'm sure you'll do a great job of informing everyone! I believe in you, Private!",
            "",

            //if loss, cur_msg = 17
            "Oh no! Shigella have taken over our host; we've lost the battle!",
            "This is a minor victory for Shigella today, but in the grand scheme of things, humans are at a greater risk.",
            "More bacteria, not just Shigella, will continuously mutate and cause harm to humans.",
            "It's not yet too late for us to change our ways, Private!",
            "Try again once more; I'm sure you'll do a great job!",
            "" //automatically go to the main menu after this message
        };

        Dialogue();
    }

    public void NextMessage() {
        try {
            dialogueTextArea.text = msgs[++cur_msg];
        }
        catch (Exception e) {
            throw (e);
        }
        //set Penny's face
        if (cur_msg == 0 || cur_msg == 1) {
            angry.SetActive(true);
            normal.SetActive(false);
        }
        else {
            angry.SetActive(false);
            normal.SetActive(true);
        }

        //events
        if (cur_msg == 4) // first dialogue is over
            Dialogue();
        if (cur_msg == 8) { // first wave is over, unfreeze time scale, spawn trigger
            Dialogue();
            freeze = false;

            {
                spawnWaveLVLS[waveCounter - 1].SetActive(false);
                pill.transform.position = loadouts[(int)UnityEngine.Random.Range(0, loadouts.Length)].transform.position; /* Randomize position */
                pill.SetActive(true); /* activate */
                screenTimer.color = Color.red; /* show lifetime timer */
                loadoutIndicator.SetActive(true);/* activate indicator */
                loadoutIndicator.transform.localScale = defaultScale;
                loadoutIndicator.transform.position = pill.transform.position; /* set indicator position to where pill is */
                plifetime = 0; /* set pill lifetime to 0 */
                waspill = true; /* pill spawned */
            }
        }

    }

    void Dialogue() {
        // Set Penny's animation to idle
        dialogues.SetActive(dialogues.activeInHierarchy ? false : true);
        if (dialogues.activeInHierarchy) NextMessage();
        c_hud.SetActive(dialogues.activeInHierarchy ? false : true);
        c_controls.SetActive(dialogues.activeInHierarchy ? false : true);
        for(int i = 0; i < waveCounter; i++) {
            spawnWaveLVLS[i].SetActive(spawnWaveLVLS[i].activeInHierarchy ? false : true);
        }
        gameObject.GetComponent<ResitanceCalculator>().enabled = gameObject.GetComponent<ResitanceCalculator>().isActiveAndEnabled ? false : true;
        if (freeze) Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }


    void OnEnable() {
        Time.timeScale = 1;
        EnemyHealth.Dead += AddEnemyCount;
        bossFight = false;
    }

    void OnDisable() {
        EnemyHealth.Dead -= AddEnemyCount;
    }

    void Update() {
        /* Update Timers */
        globalTime += Time.deltaTime;

        /* Dialogues */
        if (kills >= GAME.NUM_BACTERIA_WAVE[waveCounter-1] && waveCounter == 1 && !w1) {//wave 1 over
            w1 = true;
            freeze = true;
            Dialogue();
        }

        /* Health Pickup Management */
        // Once the acid rises and subsides acidCyclesPerHealthPickup times, spawn the health pickup if it hasn't spawned yet
        if (acidCycleCounter == GAME.acidCyclesPerHealthPickup && !healthPickup.activeInHierarchy) {
            /* Health Pickup */
            healthPickup.SetActive(true);
            healthPickup.transform.position = health[(int)UnityEngine.Random.Range(0, health.Length)].transform.position;
            /* Indicator */
            healthPickupIndicator.SetActive(true);
            healthPickupIndicator.transform.localScale = defaultScale;
            healthPickupIndicator.transform.position = healthPickup.transform.position;
            hplifetime = 0; /* reset lifetime tracker */
        }
        // Health pickup indicator resizing and shite
        if (healthPickup.activeInHierarchy) {
            hplifetime += Time.deltaTime; /* update the hp lifetime while it's active */
            if (healthPickupIndicator.transform.localScale.x >= 0) healthPickupIndicator.transform.localScale -= new Vector3(GAME.loadoutIndicatorDecaySpeed, GAME.loadoutIndicatorDecaySpeed, 0);
            else healthPickupIndicator.SetActive(false);
            // If the player fails to pick up the health thingy within its lifteime delete it from the scene
            if (hplifetime > GAME.loadoutLifetime) {
                healthPickup.SetActive(false); /* despawn pickup */
                acidCycleCounter = 0; /* reset acid cycle counter for the next pickup */
            }
        }


        ////////////////////////////////////////////////////////
        /* Research Lab Pill Management */
        if (!bossFight) {
            /*
                Rescale the pill marker while it's active
                Also update the timer which indicates:
                    > Pill lifetime
                    > Time remaining for current wave (in seconds)
            */
            if (pill.activeInHierarchy) {
                plifetime += Time.deltaTime; /* update the pill lifetime while it's active */

                int timeRemaining = (int)GAME.PillPickupTimeLimit - (int)plifetime;                
                if (loadoutIndicator.transform.localScale.x >= 0) loadoutIndicator.transform.localScale -= new Vector3(GAME.loadoutIndicatorDecaySpeed, GAME.loadoutIndicatorDecaySpeed, 0);
                else {
                    loadoutIndicator.SetActive(false);
                }
                string min = Mathf.Floor( timeRemaining/ 60).ToString("00");
                string sec = ( timeRemaining % 60).ToString("00");
                screenTimer.text = min + ":" + sec;
            }

            if (plifetime > GAME.PillPickupTimeLimit || (!pill.activeInHierarchy && waspill)) {
                screenTimer.text = "";
                plifetime = 0; 
                screenTimer.color = defaultColor;
                pill.SetActive(false);
                waspill = false;
				NextWaveStart ();
            }

        }
    }

    public void Reset() {
        Time.timeScale = 1;
        waveTimeInSeconds = 60 * GAME.waveTimeInMins;
        globalTime = 0;
        waveTime = 0;
        waveCounter = 1;
        kills = 0;
        pill.SetActive(false);
        enemyCountSlider.value = 0;
        enemyCountSlider.maxValue = GAME.NUM_BACTERIA_WAVE[0];
        screenTimer.color = defaultColor;
    }

    // int timeRemaining = (int)waveTimeInSeconds - (int)waveTime;

    public void AddEnemyCount(int points) {
        kills += points;
        if (kills >= enemyCountSlider.maxValue && !bossFight) {
            enemyCountSlider.value = enemyCountSlider.maxValue;
            if (!pill.activeInHierarchy && waveCounter != 1) {
                spawnWaveLVLS[waveCounter - 1].SetActive(false);
                pill.transform.position = loadouts[(int)UnityEngine.Random.Range(0, loadouts.Length)].transform.position; /* Randomize position */
                pill.SetActive(true); /* activate */
                screenTimer.color = Color.red; /* show lifetime timer */
                loadoutIndicator.SetActive(true);/* activate indicator */
                loadoutIndicator.transform.localScale = defaultScale;
                loadoutIndicator.transform.position = pill.transform.position; /* set indicator position to where pill is */
                plifetime = 0; /* set pill lifetime to 0 */
                waspill = true; /* pill spawned */
        	}
        }
        else {
            enemyCountSlider.value = kills;
        }
    }

    public void NextWaveStart() {
        waveCounter++;
		if (waveCounter > 3) {
			bossFight = true;
			//enable boss health bar, disable level timer
			screenTimer.gameObject.SetActive (false);
			if (enemyCountSlider.isActiveAndEnabled)
				enemyCountSlider.gameObject.SetActive (false);
			//spawn boss if not already there
			if (!Shigellang_Dormant.activeInHierarchy && bossDormant) {
				Shigellang_Dormant.SetActive (true);
				bossDormant = false;
			}
            bossSpawnDefense.SetActive(true);
		} 
		else {
			kills = 0;
			enemyCountSlider.value = 0;
			enemyCountSlider.maxValue = GAME.NUM_BACTERIA_WAVE [waveCounter - 1];
            foreach(var wave in spawnWaveLVLS) {
                wave.SetActive(false);
            }
            spawnWaveLVLS[waveCounter - 1].SetActive(true);
		}
    }

    
}
