using UnityEngine;
using System.Collections;
using GLOBAL;
public class FlyingShigella : MonoBehaviour {
    public GameObject projectile;
    
    public Transform fireposition;
    GameObject projectile_parent;
    Animator anim;
    bool isAttacking;
    float dir;
    float timerShoot;
    float currDist;
    Vector2 patrolDir;
    Vector2 attackDir;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
        projectile_parent = GameObject.Find("Projectile Parent");
        //parent = GetComponentInParent<Transform>();
        dir = 0;
        currDist = 0;
        timerShoot = 0;
        isAttacking = false;
        patrolDir = Random.insideUnitCircle;
	}
	

    void Update() {
        if (!isAttacking) {

            rb.velocity = patrolDir * GAME.FlyingShigella_mvspd;
            //transform.Translate((new Vector3(patrolDir.x, patrolDir.y, 0) * GAME.FlyingShigella_mvspd * Time.deltaTime));
            currDist += GAME.FlyingShigella_mvspd * Time.deltaTime;
        }
        else {
            rb.velocity = Vector2.zero;
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
