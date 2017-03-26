using UnityEngine;
using System.Collections;

public class MinimapScaler : MonoBehaviour {

    public Camera cam;
    public GameObject pos;
    //float width, height;
    private float itsWidth;
    private float itsHeight;

    // Use this for initialization
    void Start () {
        cam.orthographicSize = 5.38f;
        itsHeight = 1.0f / 4.0f; //set the minimap height to 1/3 of the screen
        float itsScreenHeightInPixels = Screen.height * itsHeight;
        float itsScreenWidthInPixels = (itsScreenHeightInPixels / 9.0f) * 23.0f;
        itsWidth = itsScreenWidthInPixels / Screen.width;
        float anX = (1 - itsWidth) / 2.0f; //centers the minimap
        cam.transform.position = pos.transform.position;
        cam.rect = new Rect(anX, .05f, itsWidth, itsHeight);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //width = Screen.width / 3.2f;
        //height = (width / 24.15f) * 9f;
        //float ratio = (float)Screen.width / (float)Screen.height;
        ////cam.pixelRect = new Rect(Screen.width - (width + 20), Screen.height - (height + 45f * ratio), width, height);
        //cam.pixelRect = new Rect((Screen.width - width) / 2, 25f * ratio, width, height);

        
    }
}
