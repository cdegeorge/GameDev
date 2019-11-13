using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{
    public Text rockText;
    public Text stickText;

    private string pileInRange;
    private int rockNumber = 0;
    private int stickNumber = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && pileInRange != "")
        {
            if (pileInRange == "RockPile")
            {
                rockNumber++;
                rockText.text = "Rocks: " + rockNumber;
            }
            else if (pileInRange == "StickPike")
            {
                stickNumber++;
                stickText.text = "Sticks: " + stickNumber;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RockPile"))
            pileInRange = other.gameObject.name;
        else if (other.CompareTag("StickPile"))
            pileInRange = other.gameObject.name;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RockPile") || other.CompareTag("StickPile"))
        {
            pileInRange = "";
        }
    }
}
