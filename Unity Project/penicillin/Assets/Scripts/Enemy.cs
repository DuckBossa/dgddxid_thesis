using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
    public LayerMask enemyMask;
    public bool isStunned;
    public Canvas c;
    //SpriteRenderer psprite;
    Animator myAnim;
    public GameObject player;
    PlayerHealth playerHealth;
    Rigidbody2D myBody;
    //Rigidbody2D playerBody;
    Transform myTrans, penny;
  
    bool withinRange, isAggro;
    float atkRange, aggressionDistance, myWidth, myHeight, timer, speed, timeBetweenAttacks, stunDuration, stunTimer;
    EnemyHealth enemyHealth;


    void Start() {
        playerHealth = player.GetComponent<PlayerHealth>();
        penny = player.transform;

        enemyHealth = GetComponent<EnemyHealth>();
        myTrans = gameObject.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
        myAnim = GetComponent<Animator>();
        timeBetweenAttacks = 1f;
        aggressionDistance = 2f;
        atkRange = .25f;
        speed = .5f;
        isAggro = false;
        withinRange = false;
        stunDuration = .5f;
        isStunned = false;
    }

    void Update() {
        timer += Time.deltaTime;
        if (isStunned) stunTimer += Time.deltaTime;
        if (stunTimer > stunDuration) isStunned = false;
    }

    public void Stun() {

    }

    void FixedUpdate() {
        Vector2 lineCastPos = toVector2(myTrans.position) - toVector2(myTrans.right) * myWidth + Vector2.up * myHeight;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - toVector2(myTrans.right) * .05f, enemyMask);
        if (!isGrounded || isBlocked) {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        if (!isAggro) { //patrolling
            myAnim.SetBool("withinRange", false);
            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * speed;
            myBody.velocity = myVel;

            if (Mathf.Abs(myTrans.position.x - penny.position.x) < aggressionDistance && Mathf.Abs(myTrans.position.y - penny.position.y) < 0.4f) {
                isAggro = true;
                myAnim.SetBool("isAggro", isAggro);
            }
        }
        else {
            float dist = Mathf.Abs(penny.position.x - myTrans.position.x); //distance between penny and enemy
            withinRange = dist < atkRange ? true : false; //am I gonna play the attack animation or not?
            myAnim.SetBool("withinRange", withinRange); //inform the animator to play the attack animation
            if (dist > aggressionDistance || Mathf.Abs(myTrans.position.y - penny.position.y) > 0.4f) {
                isAggro = false;
                myAnim.SetBool("isAggro", isAggro); //to change animation, go back to idle/roam
            }

            //penny right
            //bool facingRight = false;
            if (penny.position.x - myTrans.position.x > 0) {
                if (!withinRange) { //chasing after penny (aggressive but not in range yet)
                    Vector2 myVel = myBody.velocity;
                    myVel.x = myTrans.right.x * speed * 2;
                    //if the bacteria is moving towards penny
                    if (myVel.x < 0) {
                        myVel.x *= -1;
                    }
                    else {
                        //if the bacteris was facing away from penny when she entered aggresionDistance
                        Vector3 currRot = myTrans.eulerAngles;
                        currRot.y += 180;
                        myTrans.eulerAngles = currRot;
                    }
                    myBody.velocity = myVel;
                }
                else {//in attack range
                    //stop moving and attack
                    Vector2 myVel = myBody.velocity;
                    myVel.x = 0;
                    myBody.velocity = myVel;
                    if (timer > timeBetweenAttacks) Attack();
                    //rotate the sprite to the proper orientation
                    Vector3 currRot = myTrans.eulerAngles;
                    currRot.y += 180;
                    myTrans.eulerAngles = currRot;
                }
            }
            //penny left
            else {
                if (!withinRange) { //chasing after penny (aggressive but not in range yet)
                    Vector2 myVel = myBody.velocity;
                    myVel.x = myTrans.right.x * speed * 2;
                    //if the bacteria is moving towards penny
                    if (myVel.x > 0) {
                        myVel.x *= -1;
                    }
                    else {
                        //if the bacteris was facing away from penny when she entered aggresionDistance
                        Vector3 currRot = myTrans.eulerAngles;
                        currRot.y += 180;
                        myTrans.eulerAngles = currRot;
                    }
                    myBody.velocity = myVel;
                }
                else { //in attack range
                    //stop moving and attack
                    Vector2 myVel = myBody.velocity;
                    myVel.x = 0.0f;
                    myBody.velocity = myVel;
                    if(timer > timeBetweenAttacks && enemyHealth.currHealth > 0) Attack();
                    //rotate the sprite to the proper orientation
                    Vector3 currRot = myTrans.eulerAngles;
                    currRot.y += 180;
                    myTrans.eulerAngles = currRot;
                }
            }

            if(isStunned) {
                Vector2 myVel = myBody.velocity;
                myVel.x = 0f;
                myBody.velocity = myVel;
            }
        }
        c.GetComponent<RectTransform>().localRotation = myTrans.rotation;   
    }

    Vector2 toVector2(Vector3 vec3) {
        return new Vector2(vec3.x, vec3.y);
    }

    void Attack() {
        timer = 0f;
        if (playerHealth.currHealth > 0 ) {
            //knockback
            //playerBody.AddForce(Vector2.up * 50f);
            //playerBody.AddForce(Vector2.right * 1000f);
            playerHealth.TakeDamage();
        }
    }
}
