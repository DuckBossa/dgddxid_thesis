using UnityEngine;
using UnityEngine.UI;
using GLOBAL;
using System.Collections;

public class StomachLevel_Global : MonoBehaviour {
    // Use this for initialization

        /*
         To-do:
         Somehow count the number of waves to determine when to change the value of bossFight boolean
         Adjust the required number of enemies to eliminate per wave
         */
    public Text screenTimer;
    public Slider enemyCountSlider;
    public static float globalTime;
    public Transform[] loadouts;
    public Transform[] health;
    public Transform player;
    public int kills;
    public GameObject pill,
        loadoutIndicator,
        Shigellang_Dormant,
        healthPickup, healthPickupIndicator,
        dialogues,
        c_controls,
        c_hud;
    public int acidCycleCounter;
    bool bossFight;
    bool bossDormant;

    private float waveTimeInSeconds, waveTime, levelTime, hplifetime, plifetime;
    private bool waspill;
    private Color defaultColor;
    private Vector3 defaultScale;
    private int waveCounter;
    
	void Start () {
		waveTimeInSeconds = 60 * GAME.waveTimeInMins; /* Duration of one wave */
		levelTime = 60 * GAME.waveTimeInMins * GAME.num_waves; /* Duration of the entire level without boss fight // fix this; 3, 2, 1 */
        enemyCountSlider.value = 0;
        enemyCountSlider.maxValue = GAME.num_bacteria_wave1;
        defaultColor = screenTimer.color;
        globalTime = 0; /* Time elapsed since the beginning of the level */
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
        waveCounter = 1;
        kills = 0;
        //dialogues.SetActive(true);
        //c_controls.SetActive(false);
        //c_hud.SetActive(false);
    }
	
    void OnEnable() {
        Time.timeScale = 1;
        bossFight = false;
    }

    void FixedUpdate() {
    }

	void Update () {
        /* Update Timers */
        globalTime += Time.deltaTime;
        waveTime += Time.deltaTime;

        enemyCountSlider.value = kills; //temporary update manner, there's gotta be a better way to do this

        int timeRemaining = (int)waveTimeInSeconds - (int)waveTime;

        /* Health Pickup Management */
        // Once the acid rises and subsides acidCyclesPerHealthPickup times, spawn the health pickup if it hasn't spawned yet
        if (acidCycleCounter == GAME.acidCyclesPerHealthPickup && !healthPickup.activeInHierarchy) {
            /* Health Pickup */
            healthPickup.SetActive(true);
            healthPickup.transform.position = health[(int)Random.Range(0, health.Length)].transform.position;
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

        // Only spawn the pill when not in boss fight
        if (!bossFight) {
            //if the time remaining in the wave is enough for the lifetime of the pill, spawn the pill
            if (!pill.activeInHierarchy && waveTimeInSeconds - waveTime <= GAME.loadoutLifetime ) {
                pill.transform.position = loadouts[(int)Random.Range(0, loadouts.Length)].transform.position; /* Randomize position */
                pill.SetActive(true); /* activate */
                screenTimer.color = Color.red; /* show lifetime timer */
                loadoutIndicator.SetActive(true);/* activate indicator */
                loadoutIndicator.transform.localScale = defaultScale;
                loadoutIndicator.transform.position = pill.transform.position; /* set indicator position to where pill is */
                plifetime = 0; /* set pill lifetime to 0 */
                waspill = true; /* pill spawned */
            }

            /*
                Rescale the pill marker while it's active
                Also update the timer which indicates:
                    > Pill lifetime
                    > Time remaining for current wave (in seconds)
            */
            if (pill.activeInHierarchy) {
                plifetime += Time.deltaTime; /* update the pill lifetime while it's active */
                if (loadoutIndicator.transform.localScale.x >= 0) loadoutIndicator.transform.localScale -= new Vector3(GAME.loadoutIndicatorDecaySpeed, GAME.loadoutIndicatorDecaySpeed, 0);
                else {
                    loadoutIndicator.SetActive(false);
                }
                string min = Mathf.Floor(timeRemaining / 60).ToString("00");
                string sec = (timeRemaining % 60).ToString("00");
                screenTimer.text = min + ":" + sec;
            }

            /* Player didn't reach loadoutsection in time, go straight to next wave */
            /* Player picked up pill */
            if (plifetime > GAME.loadoutLifetime || (!pill.activeInHierarchy && waspill)) {
                globalTime = waveTimeInSeconds * waveCounter; /* reasonable, right? */
                screenTimer.text = "";
                waveTime = 0;
                plifetime = 0; /* reset value so waveTime won't always be 0 */
                waveTimeInSeconds = 60 * GAME.waveTimeInMins;
                screenTimer.color = defaultColor;
                pill.SetActive(false);
                waspill = false;
            }

            if (globalTime > levelTime) {
                bossFight = true;
                //enable boss health bar, disable level timer
                screenTimer.gameObject.SetActive(false);
                if (enemyCountSlider.isActiveAndEnabled) enemyCountSlider.gameObject.SetActive(false);
                //spawn boss if not already there
                if (!Shigellang_Dormant.activeInHierarchy && bossDormant) {
                    Shigellang_Dormant.SetActive(true);
                    bossDormant = false;
                }
            }
        }
    }

    public void Reset() {
        Time.timeScale = 1;
        waveTimeInSeconds = 60 * GAME.waveTimeInMins;
        globalTime = 0;
        waveTime = 0;
        pill.SetActive(false);
        screenTimer.color = defaultColor;
    }

    void Dialogue() {
        dialogues.SetActive(dialogues.activeInHierarchy ? false : true);
        c_hud.SetActive(dialogues.activeInHierarchy ? false : true);
        c_controls.SetActive(dialogues.activeInHierarchy ? false : true);
    }
}
