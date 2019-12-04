using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    public Text rockText;
    public Text stickText;
    public int rockPickupNumber = 5;
    public int stickPickupNumber = 3;
    public int rockNumber = 0;
    public int stickNumber = 0;
    public AudioSource[] pickups;

    void Update()
    {
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (other.CompareTag("RockPile")) {
                rockNumber += rockPickupNumber;
                rockText.text = "Rocks: " + rockNumber;
                Destroy(other.gameObject);
                pickups[0].Play();
            }
            else if (other.CompareTag("StickPile")) {
                stickNumber += stickPickupNumber;
                stickText.text = "Sticks: " + stickNumber;
                Destroy(other.gameObject);
                pickups[1].Play();
            }

        }
    }
}
