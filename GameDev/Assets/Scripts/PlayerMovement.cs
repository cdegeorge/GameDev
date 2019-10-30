using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player movement
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float lookSpeed;

    private float startTime;
    private bool isGrounded = true;
    private Rigidbody rigidbody;
    Actions actions;
    CapsuleCollider capCollider;

    private void Start()
    {
        actions = GetComponent<Actions>();
        rigidbody = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        Look();
        Jump();
        Walk();        
    }


    void Look() {
        if (Input.GetAxis("Mouse X") >= .01 || Input.GetAxis("Mouse X") <= .01)
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * lookSpeed, 0)));
    }
    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded == true) {
            isGrounded = false;
            actions.Jump();
            rigidbody.AddRelativeForce(Vector3.up * jumpSpeed);
            startTime = Time.time;
        }
        else {
            if (transform.position.y < .6f) {
                isGrounded = true;
            }
        }
    }

    void Walk() {
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
            actions.Walk();
            rigidbody.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed));
        }
        else {
            actions.Stay();
        }
    }
}
