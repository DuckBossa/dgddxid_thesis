using UnityEngine;
using System.Collections;

public class ShigellangRadar : MonoBehaviour {

    Rigidbody2D rb;
    public LayerMask mask;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(LayerMask.NameToLayer("Map"));
    }

    // Update is called once per frame
    void FixedUpdate() {
        
    }
}