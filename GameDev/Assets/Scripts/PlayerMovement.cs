using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player movement
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float lookSpeed;
    public float capSpeed = 1;
    
    private bool isGrounded = true;
    private float startTime;
    private float journeyLength = .5f;
    private float dist;
    private float fracOfJourney;
    private Vector3 start = new Vector3(0, .5f, 0);
    private Vector3 end = new Vector3(0, 1, 0);
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

     private void Update() {
        if (!isGrounded) {
            dist = (Time.time - startTime) * capSpeed;
            fracOfJourney = dist / journeyLength;
            capCollider.center = Vector3.Lerp(start, end, fracOfJourney);
        }
        else {
            capCollider.center = Vector3.Lerp(end, start, fracOfJourney);
        }
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
