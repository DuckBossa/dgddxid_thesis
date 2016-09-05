using UnityEngine;
using System;
using System.IO;
using System.Collections;

public class EventHandler : MonoBehaviour {

	public static event Action Attack;
	public static event Action<Vector3> Move;
	public static event Action Jump;
	public static event Action UseItem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
