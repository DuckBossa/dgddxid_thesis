using UnityEngine;
using System.Collections;

public class MinimapScaler : MonoBehaviour {

    public Camera cam;
    float width, height;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        width = Screen.width / 3.9f;
        height = (width / 24.15f) * 9f;
        float ratio = (float)Screen.width / (float)Screen.height;
        cam.pixelRect = new Rect(Screen.width - (width + 20), Screen.height - (height + 45f * ratio), width, height);
        cam.orthographicSize = 5.38f;
    }
}
