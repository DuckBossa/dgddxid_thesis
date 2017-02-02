using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class Tutorial_LoadoutTrigger : MonoBehaviour {

    public Canvas loadoutCanvas;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Penny") {
            loadoutCanvas.gameObject.SetActive(true);
            Debug.Log(col.gameObject.name);
            Time.timeScale = 0;
        }
    }

    public void DisableCanvas() {
        Time.timeScale = 1;
        loadoutCanvas.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
