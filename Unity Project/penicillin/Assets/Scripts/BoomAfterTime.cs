using UnityEngine;
using System.Collections;

public class BoomAfterTime : MonoBehaviour {


	Rigidbody2D rb2d;
	float currtime;
	float lifeTime;
	ClusterBomb cb;
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
		cb = GetComponent<ClusterBomb> ();
		currtime = 0;
	}

	public void Update(){

		if (currtime < lifeTime) {
			currtime += Time.deltaTime;
		} 
		else {
			cb.boom ();	
		}

	}


	public void setDir(Vector2 val) {
		rb2d.velocity = val;
	}
	public void setLifeTime(float time){
		lifeTime = time;
	}
}
