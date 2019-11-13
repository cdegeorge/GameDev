using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretPlacement : PlayerPickup
{
    public GameObject turret;

    Vector3 position;

    private void Update() {
        position = transform.position + new Vector3(0,0, 5f);
        if (Input.GetKeyDown(KeyCode.T) && rockNumber >= 5 && stickNumber >= 3) {
            Instantiate(turret, position , Quaternion.identity);
            rockNumber -= 5;
            stickNumber -= 3;
        }
    }
}
