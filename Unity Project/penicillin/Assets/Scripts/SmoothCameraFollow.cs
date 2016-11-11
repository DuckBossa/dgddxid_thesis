using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour {
	public Transform target;
    public Vector2 smoothing;
    public BoxCollider2D Bounds;
    private Vector3 min, max;
    public bool isFollowing { get; set; }
    Camera camera;

	void Start(){
        isFollowing = true;
        camera = GetComponent<Camera>();
        min = Bounds.bounds.min;
        max = Bounds.bounds.max;
	}

	// Update is called once per frame
	void FixedUpdate (){
        var x = transform.position.x;
        var y = transform.position.y;

        x = Mathf.Lerp(x, target.position.x, smoothing.x * Time.deltaTime);
        y = Mathf.Lerp(y, target.position.y, smoothing.y * Time.deltaTime);

        var cameraHalfWidth = camera.orthographicSize * ((float)Screen.width / Screen.height);

        x = Mathf.Clamp(x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);
        y = Mathf.Clamp(y, min.y + camera.orthographicSize, max.y - camera.orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}