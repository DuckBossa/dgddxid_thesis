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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDev) {
			if (Move != null) {
				Move(Input.GetAxisRaw("Horizontal"));
			}

			if 
		}

	}
}
