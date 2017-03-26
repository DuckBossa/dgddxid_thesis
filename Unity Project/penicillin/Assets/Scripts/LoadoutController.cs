using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;
using System;
public class LoadoutController : MonoBehaviour {

    public Canvas loadoutCanvas;
    public GameObject loadoutIndicator, mgr;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Penny") {
            loadoutCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
            loadoutIndicator.SetActive(false);
        }
    }

    public void DisableCanvas() {
        loadoutCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        if(mgr != null) {
            mgr.GetComponent<StomachLevel_Global>().BossDialogue();
        }
        //waves
    }
}
