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
    Actions actions;
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 1000f;          // The length of the ray from the camera into the scene.




    private void Start()
    {
        actions = GetComponent<Actions>();
        rigidbody = GetComponent<Rigidbody>();
        lastPosition = transform.position.y;
        Time.timeScale = 1;
        floorMask = LayerMask.GetMask("Floor");
    }

    private void Update()
    {
         Vector3 jump = new Vector3(0, 10, 0);

        //rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * lookSpeed, 0)));
       

        ///////////////////
        // Rotation
        Turning();

        ///////////////////
        // Mevement
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
            actions.Walk();
            rigidbody.MovePosition(transform.position + (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed));
            if (Input.GetAxis("Jump") != 0)
                actions.Jump();
        }
        else {
            actions.Stay();
        }

        jump.Set(0f, Input.GetAxis("Jump")/2, 0f);

        if (!jump.Equals(Vector3.zero) && rigidbody.velocity.y == 0 && isGrounded) {
            rigidbody.AddRelativeForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
        else {
            isGrounded = true;
        }
        //if (transform.position.y < lastPosition)
          //  rigidbody.AddForce(Vector3.down * 20, ForceMode.Force);
        lastPosition = transform.position.y;
    }

    void Turning() {
        Debug.Log("Turning called");
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(camRay);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Debug.Log("if entered");
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, .03f);
            //rigidbody.MoveRotation(newRotation);
            Debug.Log(newRotation);
        }
    }
}
