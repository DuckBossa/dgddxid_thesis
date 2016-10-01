using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class EventHandler : MonoBehaviour {

	public static event Action Attack;
	public static event Action<float> Move;
	public static event Action Jump;
	public static event Action UseItem;
	public static event Action Dash;

	public bool isDev = true;

    public float hInput = 0;
    public bool jump = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDev) {
			if (Move != null) {
                //Move(Input.GetAxisRaw("Horizontal"));
                Move(hInput);
            }
            if (Jump != null) {
                //if (Input.GetButtonDown("Jump")) {
                //    Jump();
                //}
                if (jump)
                {
                    Jump();
                    jump = false;
                }
            }
            if (UseItem != null) {
                
            }
            /*
            if (Dash != null) {
                if (Input.GetKeyDown(KeyCode.Z)){
                    Dash();
                }
            }
            */
        }

    }

    public void StartMoving(float horizontalInput)
    {
        hInput = horizontalInput;
    }

    public void StartJumping(bool jumping)
    {
        jump = jumping;
    }
}
