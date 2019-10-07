using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles player movement
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public float lookSpeed;
    
    private bool isGrounded = true;
    private Rigidbody rigidbody;
    private float lastPosition;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lastPosition = transform.position.y;
    }

    private void Update()
    {
        Vector3 jump = new Vector3();

        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * lookSpeed, 0)));
        rigidbody.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed));
        jump.Set(0f, Input.GetAxis("Jump"), 0f);

        if (!jump.Equals(Vector3.zero) && rigidbody.velocity.y == 0)
            rigidbody.AddRelativeForce(Vector3.up * jumpSpeed, ForceMode.Impulse);

        if (transform.position.y < lastPosition)
            rigidbody.AddForce(Vector3.down * 20, ForceMode.Force);
        lastPosition = transform.position.y;
    }
}
