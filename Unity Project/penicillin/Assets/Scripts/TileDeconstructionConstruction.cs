using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GLOBAL;

public class TileDeconstructionConstruction : MonoBehaviour {
	public List<GameObject> sets = new List<GameObject>();
    public GameObject animbackGO;
     
    float timer;

    int rnglul;
    Animator anim;
    Animator anim_back;

    bool stay;
    bool stop;
	void Start () {
        anim = GetComponent<Animator>();
        stop = false;
        stay = false;
        timer = 0;
		rnglul = Random.Range (0, sets.Count);
        anim_back = animbackGO.GetComponent<Animator>();
        EnableDeconstruction();
	}

    public void BossBattle() {
        stop = true;
    }


    public void Update() {
        if (stay && !stop) {
            if(timer <= GAME.acid_stay_timer) {
                timer += Time.deltaTime;
            }
            else {
                Up();
                timer = 0;
            }
        }
    }


    public void reconstruct() {
		foreach (GameObject set in sets) {
		    set.SetActive (false);
		}
		rnglul = Random.Range (0, sets.Count);
		sets [rnglul].SetActive (true); 
        foreach (Transform set in sets[rnglul].transform) {
			set.gameObject.SetActive(true);
        }
		DisableDeconstruction();
	}

    public void EnableDeconstruction() {
        if (!stop) {
            gameObject.GetComponent<TileDeconstruction>().enabled = true;
            gameObject.GetComponent<TileConstruction>().enabled = false;
        }
        else {
            anim.SetTrigger("Boss");
            anim_back.SetTrigger("Boss");
        }

    }
    public void DisableDeconstruction() {
        gameObject.GetComponent<TileConstruction>().enabled = true;
		gameObject.GetComponent<TileConstruction> ().setColliders (sets [rnglul].GetComponentsInChildren<BoxCollider2D>());
        gameObject.GetComponent<TileDeconstruction>().enabled = false;
    }
    public void Stay() {
        anim.SetTrigger("Stay");
        anim_back.SetTrigger("Stay");
        stay = true;
    }
    public void Up() {
        anim_back.SetTrigger("Up");
        anim.SetTrigger("Up");
        stay = false;
    }
}
