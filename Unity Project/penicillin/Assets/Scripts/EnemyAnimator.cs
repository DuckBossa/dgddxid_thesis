using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour {

    public static EnemyAnimator instance;
    Transform myTrans;
    Animator myAnim;

	void Start () {
        instance = this;
        myTrans = this.transform;
        myAnim = this.gameObject.GetComponent<Animator>();
	}

    public void UpdateIsAggro(bool aggro) {
        myAnim.SetBool("isAggro", aggro);
    }

    public void UpdateRange(float currentRange) {
        myAnim.SetFloat("atkRange", currentRange);
    }
}
