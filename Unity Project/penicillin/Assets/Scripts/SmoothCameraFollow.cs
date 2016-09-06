using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	// copy pastarina from http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html

	public float dampTime = 0.15f;
	public Transform target;
	private Camera camera;
	private Vector3 velocity = Vector3.zero;

	void Start(){
		camera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update (){
		if (target) {
			Vector3 point = camera.WorldToViewportPoint (target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint (new Vector3 (0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
		}
	}
}