using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Dialogues_Stomach : MonoBehaviour {
    public GameObject[] spawnmgrs;
    public GameObject[] hudObjects;
    public Text text;

    private string[] messages;
    private int cur_msg;
        
	// Use this for initialization
	void Start () {
        foreach(GameObject g in spawnmgrs) {
            g.SetActive(false);
        }

        ToggleCanvas();

        messages = new string[] {
            "Oh no!",
            "Salmonella have invaded our host's stomach; we have to get them out of here!"
        };
	}

	// Update is called once per frame
	void Update () {
	    
	}

    public void NextDialogue() {
        try {
            text.text = messages[++cur_msg];
        }
        catch (Exception e) {
            throw(e);
        }
    }

    void ToggleCanvas() {
        foreach(GameObject g in hudObjects) {
            g.SetActive(g.activeInHierarchy ? false : true);
        }
    }
}
