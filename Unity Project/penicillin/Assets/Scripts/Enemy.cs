using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans, penny;
    float myWidth, myHeight;
    public float aggressionDistance;
    public bool isAggro;
    bool withinRange;
    public float atkRange;
    SpriteRenderer psprite;
    Animator myAnim;


    void Start() {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;

        GameObject p = GameObject.Find("Penny");
        penny = p.transform;
        psprite = p.GetComponent<SpriteRenderer>();
        aggressionDistance = 2f;
        atkRange = .25f;
        speed = .5f;
        isAggro = false;
        withinRange = false;
        myAnim = GetComponent<Animator>();
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
        if (!isAggro) {
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
            myAnim.SetBool("withinRange", withinRange); //let the animator know that it's attack time
            if (dist > aggressionDistance || Mathf.Abs(myTrans.position.y - penny.position.y) > 0.4f) {
                isAggro = false; 
                myAnim.SetBool("isAggro", isAggro); //to change animation, go back to idle/roam
            }

            //penny right
            bool facingRight = false;
            if (penny.position.x - myTrans.position.x > 0) {
                if (!withinRange) {
                    Vector2 myVel = myBody.velocity;
                    myVel.x = myTrans.right.x * speed * 2;
                    //if the bacteria is moving towards penny
                    if (myVel.x < 0) {
                        myVel.x *= -1;
                    }
                    else {
                        Vector3 currRot = myTrans.eulerAngles;
                        currRot.y += 180;
                        myTrans.eulerAngles = currRot;
                    }
                    myBody.velocity = myVel;
                }
                else {
                    Vector2 myVel = myBody.velocity;
                    myVel.x = 0;
                    myBody.velocity = myVel;

                    Vector3 currRot = myTrans.eulerAngles;
                    currRot.y += 180;
                    myTrans.eulerAngles = currRot;
                }
            }


            //penny left
            else {
                if (!withinRange) {
                    Vector2 myVel = myBody.velocity;
                    myVel.x = myTrans.right.x * speed * 2;
                    //if the bacteria is moving towards penny
                    if (myVel.x > 0) {
                        myVel.x *= -1;
                    }
                    else {
                        Vector3 currRot = myTrans.eulerAngles;
                        currRot.y += 180;
                        myTrans.eulerAngles = currRot;
                    }
                    myBody.velocity = myVel;
                }
                else {
                    Vector2 myVel = myBody.velocity;
                    myVel.x = 0;
                    myBody.velocity = myVel;

                    Vector3 currRot = myTrans.eulerAngles;
                    currRot.y += 180;
                    myTrans.eulerAngles = currRot;
                }
            }
        }
    }

    Vector2 toVector2(Vector3 vec3) {
        return new Vector2(vec3.x, vec3.y);
    }
}
