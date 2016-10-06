using UnityEngine;
using System.Collections;
using GLOBAL;
public class FlyingShigella : MonoBehaviour {
    public GameObject projectile;
    public GameObject projectile_parent;
    public Transform fireposition;
    Animator anim;
    bool isAttacking;
    float dir;
    float timerShoot;
    float currDist;
    Vector2 patrolDir;
    Vector2 attackDir;
	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
        //parent = GetComponentInParent<Transform>();
        dir = 0;
        currDist = 0;
        timerShoot = 0;
        isAttacking = false;
        patrolDir = Random.insideUnitCircle;
	}
	

    void Update() {
        if (!isAttacking) {
            transform.Translate((new Vector3(patrolDir.x, patrolDir.y, 0) * GAME.FlyingShigella_mvspd * Time.deltaTime));
            currDist += GAME.FlyingShigella_mvspd * Time.deltaTime;
        }

        anim.SetBool("isAttacking", isAttacking);
        anim.SetFloat("Direction", patrolDir.x > 0 ? 1 : 0);
        if (currDist > GAME.FlyingShigella_patrolDist) {
            currDist = 0;
            patrolDir = Random.insideUnitCircle;
        }
    }


    public void isAttack(bool val) {
        isAttacking = val;
    }

    public void setAttackDir(Vector2 dir) {
        attackDir = dir;
    }

    void Attack() {
        GameObject temp = Instantiate(projectile,fireposition.position,Quaternion.identity) as GameObject;
        temp.GetComponent<MoveDir>().setDir(attackDir);
        temp.transform.parent = projectile_parent.transform;
    }
}
