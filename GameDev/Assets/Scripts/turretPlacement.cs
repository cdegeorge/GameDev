using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretPlacement : MonoBehaviour
{
    public GameObject turret;
    public GameObject playerPickupObject;
    public Transform objSpawn;

    private Vector3 position;
    private PlayerPickup playerPickup;

    private void Start()
    {
        playerPickup = playerPickupObject.GetComponent<PlayerPickup>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T) && playerPickup.rockNumber >= 15 && playerPickup.stickNumber >= 18) {
            position = objSpawn.position;

            Instantiate(turret, position , Quaternion.identity);
            playerPickup.rockNumber -= 15;
            playerPickup.stickNumber -= 18;
            playerPickup.rockText.text = "Rocks: " + playerPickup.rockNumber;
            playerPickup.stickText.text = "Sticks: " + playerPickup.stickNumber;
        }
    }
}
