using UnityEngine;
using System.Collections;
using GLOBAL;

public class MoveDir : MonoBehaviour {

    // Use this for initialization
    Vector2 dir;
    Rigidbody2D rb2d;
    float currtime;
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        currtime = 0;
	}
	
	void Update () {
        if (currtime > GAME.FlyingShigella_projlife) {
            Destroy(gameObject);
        }
        else {
            Debug.Log(rb2d.velocity);
            
            //transform.Translate(new Vector3(dir.x, dir.y, 0) * GAME.FlyingShigella_projspeed * Time.deltaTime);

            currtime += Time.deltaTime;
        }
	}
    public void setDir(Vector2 val) {
        rb2d.velocity = val * 4;
    }
}
