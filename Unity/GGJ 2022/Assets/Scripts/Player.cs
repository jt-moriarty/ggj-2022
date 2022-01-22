using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Rigidbody mRigidbody;
    float moveSpeed = 20f;
    float jumpSpeed = 10f;

    // Start is called before the first frame update
    void Start () {
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        // Just some dumb player move code to test the cameras, purge later.
        if (Input.GetKey(KeyCode.A)) {
            mRigidbody.AddForce(Vector3.left * Time.deltaTime * moveSpeed, ForceMode.VelocityChange);
        }
        else if (Input.GetKey(KeyCode.D)) {
            mRigidbody.AddForce(Vector3.right * Time.deltaTime * moveSpeed, ForceMode.VelocityChange);
        }

        // Test jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            mRigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }
    }
}
