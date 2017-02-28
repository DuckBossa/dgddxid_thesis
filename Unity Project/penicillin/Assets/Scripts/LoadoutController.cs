using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GLOBAL;

public class LoadoutController : MonoBehaviour {

    public Canvas loadoutCanvas;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Penny") {
            loadoutCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            gameObject.SetActive(false);
        }
    }

    public void DisableCanvas() {
        loadoutCanvas.gameObject.SetActive(false);
    }
}
