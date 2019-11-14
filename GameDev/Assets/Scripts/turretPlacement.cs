using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretPlacement : MonoBehaviour
{
    public GameObject turret;
    public GameObject playerPickupObject;

    private Vector3 position;
    private PlayerPickup playerPickup;

    private void Start()
    {
        playerPickup = playerPickupObject.GetComponent<PlayerPickup>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T) && playerPickup.rockNumber >= 5 && playerPickup.stickNumber >= 3) {
            position = transform.position + new Vector3(0, 0, 5f);

            Instantiate(turret, position , Quaternion.identity);
            playerPickup.rockNumber -= 5;
            playerPickup.stickNumber -= 3;
        }
    }
}
