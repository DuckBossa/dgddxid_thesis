using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadoutController : MonoBehaviour {

    public Canvas loadoutCanvas;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("sfd");
        Time.timeScale = 0;
        loadoutCanvas.gameObject.SetActive(true);
    }

    public void DisableCanvas() {
        loadoutCanvas.gameObject.SetActive(false);
    }
}
