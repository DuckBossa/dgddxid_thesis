using UnityEngine;
using GLOBAL;
using System.Collections.Generic;
using System.Collections;

public class ResitanceCalculator : Singleton<ResitanceCalculator> {


    public static ResitanceCalculator Instance {
        get {
            return ((ResitanceCalculator)mInstance);
        }
        set {
            mInstance = value;
        }
    }


    float[] resistance = new float[3];
    float timer;
    
	void Start () {
        timer = 0;
        for(int i = 0; i < resistance.Length; i++) {
            ResetResitance(i);
        }
	}


	void Update () {
        timer += Time.deltaTime;
        if(timer >= GAME.resitanceTickTimer) {
            AddResistance();
        }
	}


    void ResetResitance(int weapID) {
        resistance[weapID] = 0;
    }

    void AddResistance() {
        timer = 0;
        for (int i = 0; i < resistance.Length; i++) {
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
