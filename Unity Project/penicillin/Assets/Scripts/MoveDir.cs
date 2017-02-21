using UnityEngine;
using System.Collections;
using GLOBAL;

public class MoveDir : MonoBehaviour {
  
    Rigidbody2D rb2d;
    float currtime;
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
        currtime = 0;
	}

    public void setDir(Vector2 val) {
        rb2d.velocity = val;
    }
}
