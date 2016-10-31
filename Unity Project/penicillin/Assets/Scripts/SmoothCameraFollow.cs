using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {

	// copy pastarina from http://answers.unity3d.com/questions/29183/2d-camera-smooth-follow.html

	public float speed = 0.15f;
    public float mapminx, mapmaxx, mapminy, mapmaxy;
	public Transform target;
	Camera camera;

	void Start(){
		camera = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update (){
        if (target) {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, mapminx, mapmaxx),
                Mathf.Clamp(target.position.y, mapminy, mapmaxy),
                target.position.z - 20);
        }
	}
}