using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public LayerMask enemyMask;
    public float speed;
    Rigidbody2D myBody;
    Transform myTrans, penny;
    float myWidth, myHeight;
    public float aggressionDistance;
    public bool isAggro = false;
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
        atkRange = .15f;
        speed = .5f;
        myAnim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (!isAggro) {
            Vector2 lineCastPos = toVector2(myTrans.position) - toVector2(myTrans.right) * myWidth + Vector2.up * myHeight;
            bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
            bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - toVector2(myTrans.right) * .05f, enemyMask);
            if (!isGrounded || isBlocked) {
                Vector3 currRot = myTrans.eulerAngles;
                currRot.y += 180;
                myTrans.eulerAngles = currRot;
            }
            Vector2 myVel = myBody.velocity;
            myVel.x = -myTrans.right.x * speed;
            myBody.velocity = myVel;

            if (Mathf.Abs(myTrans.position.x - penny.position.x) < aggressionDistance && Mathf.Abs(myTrans.position.y - penny.position.y) < 0.4f) {
                isAggro = true;
                myAnim.SetBool("isAggro", isAggro);
            }
        }
        else {
            atkRange = Mathf.Abs(penny.position.x - myTrans.position.x);
            if (atkRange > aggressionDistance || Mathf.Abs(myTrans.position.y - penny.position.y) > 0.4f) {
                isAggro = false;
                myAnim.SetBool("isAggro", isAggro);

            }
            //penny left
            if (penny.position.x - myTrans.position.x > 0) {
                Vector2 myVel = myBody.velocity;
                myVel.x = myTrans.right.x * speed * 2;
                myBody.velocity = myVel;
            }
            //penny right
            else {
                Vector2 myVel = myBody.velocity;
                myVel.x = -myTrans.right.x * speed * 2;
                myBody.velocity = myVel;
            }
        }
    }

    Vector2 toVector2(Vector3 vec3) {
        return new Vector2(vec3.x, vec3.y);
    }
}
