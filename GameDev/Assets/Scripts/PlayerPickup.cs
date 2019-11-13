using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    public Text rockText;
    public Text stickText;

    private int rockNumber = 0;
    private int stickNumber = 0;

    void Update()
    {
    }
 
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (other.CompareTag("RockPile")) {
                rockNumber++;
                rockText.text = "Rocks: " + rockNumber;
                Destroy(other.gameObject);
            }
            else if (other.CompareTag("StickPile")) {
                stickNumber++;
                stickText.text = "Sticks: " + stickNumber;
                Destroy(other.gameObject);
            }

        }
    }
}
