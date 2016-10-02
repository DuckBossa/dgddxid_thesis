using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public LayerMask enemyMask;
    public float speed = 1;
    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;

    void Start() {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
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
        Vector2 myVel = myBody.velocity;
        myVel.x = -myTrans.right.x * speed;
        myBody.velocity = myVel;
    }

    Vector2 toVector2(Vector3 vec3) {
        return new Vector2(vec3.x, vec3.y);
    }
}
