using UnityEngine;
using System.Collections;
using GLOBAL;

public class MoveDir : MonoBehaviour {

    // Use this for initialization
    Vector2 dir;
    float currtime;
	void Start () {
        currtime = 0;
	}
	
	void Update () {
        if (currtime > GAME.FlyingShigella_projlife) {
            Destroy(gameObject);
        }
        else {
            transform.Translate(new Vector3(dir.x, dir.y, 0) * GAME.FlyingShigella_projspeed * Time.deltaTime);
            currtime += Time.deltaTime;
        }
	}
    public void setDir(Vector2 val) {
        dir = val;
    }
}
