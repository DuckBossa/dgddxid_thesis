using UnityEngine;
using System.Collections;
using GLOBAL;
public class FlyingShigella : MonoBehaviour {
    public GameObject projectile;
    public Transform fireposition;

    EnemyHealth eh;

    GameObject projectile_parent;
    Animator anim;
    bool isAttacking;
    //float dir;
    //float timerShoot;
    float currDist;
    float attackTimer;
    Vector2 patrolDir;
    Vector2 attackDir;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        eh = GetComponent<EnemyHealth>();
        anim = GetComponentInParent<Animator>();
        projectile_parent = GameObject.Find("Projectile Parent");
        //parent = GetComponentInParent<Transform>();
        //dir = 0;
        currDist = 0;
        attackTimer = GAME.FlyingShigella_aspd;
        //timerShoot = 0;
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
            if(attackTimer >= GAME.FlyingShigella_aspd) {
                if (!eh.amIDead()) {
                    anim.SetTrigger("Attack");
                    attackTimer = 0;
                }

            }
            else {
                attackTimer += Time.deltaTime;
            }
            rb.velocity = Vector2.zero;

        }
	
        if (currDist > GAME.FlyingShigella_patrolDist) {
            currDist = 0;
            patrolDir = Random.insideUnitCircle;
        }

		if (isAttacking) {
			anim.SetFloat ("Direction", attackDir.x > 0 ? 1 : 0);
		} 
		else {
			anim.SetFloat("Direction", patrolDir.x > 0 ? 1 : 0);
		}
    }


    public void isAttack(bool val) {
        isAttacking = val;
    }

    public void setAttackDir(Vector2 dir) {
        attackDir = dir;
    }

    void Attack() {
        GameObject temp = Instantiate(projectile, fireposition.position, Quaternion.identity) as GameObject;
		temp.GetComponent<MoveDir>().setDir(attackDir * GAME.FlyingShigella_projspeed);
        temp.transform.parent = projectile_parent.transform;
    }
}
