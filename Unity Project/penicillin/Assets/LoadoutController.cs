using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadoutController : MonoBehaviour {

    public Canvas loadoutCanvas;

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Penny") {
            Time.timeScale = 0;
            loadoutCanvas.gameObject.SetActive(true);
        }
    }

    public void DisableCanvas() {
        loadoutCanvas.gameObject.SetActive(false);
    }
}
