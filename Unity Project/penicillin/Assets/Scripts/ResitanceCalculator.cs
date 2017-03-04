using UnityEngine;
using GLOBAL;
using System.Collections.Generic;
using System.Collections;

public class ResitanceCalculator : MonoBehaviour {

    float[] resistance = new float[3];
    float timer;
    
	void Start () {
        timer = 0;
        ResetResitance();
	}


	void Update () {
        timer += Time.deltaTime;
        if(timer >= GAME.resitanceTickTimer) {
            AddResistance();
        }
	}


    void ResetResitance() {
        for (int i = 0; i < resistance.Length; i++) {
            resistance[i] = 0;
        }
    }

    void AddResistance() {
        timer = 0;
        for (int i = 0; i < resistance.Length; i++) {
            //might change, since htis is a linear increase in resistance. might not reset time anymore at this point
            resistance[i] += GAME.RESISTANCE_TICK[i];
            if(resistance[i] >= GAME.peakResist) {
                resistance[i] = GAME.peakResist;
            }
        }
    }

    public float GetResistanceModifier(int weap_id) {
        return resistance[weap_id];
    }
}
