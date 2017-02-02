using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Tutorial_LoadoutTrigger : MonoBehaviour {

    public Canvas loadoutCanvas;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Penny") {
            Time.timeScale = 0;
            loadoutCanvas.gameObject.SetActive(true);
        }
    }

    public void DisableCanvas() {
        Time.timeScale = 1;
        loadoutCanvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
