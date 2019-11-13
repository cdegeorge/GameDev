using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public GameObject stickPile;
    public GameObject rockPile;

    private bool isDead = false;

    void Awake() {
        currentHealth = health;
    }

    
    public void TakeDamage(float amount /*Vector3 hitPoint*/) {
        if(isDead) {
            return;
        }
        currentHealth -= amount;

        if (currentHealth <= 0) {

            if (gameObject.CompareTag("Tree"))
            {
                Vector3 position = gameObject.transform.position;
                Instantiate(stickPile, new Vector3(position.x, -5.07f, position.z), gameObject.transform.rotation);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Rock"))
            {
                Vector3 position = gameObject.transform.position;
                Instantiate(rockPile, new Vector3(position.x, -22.6f, position.z), gameObject.transform.rotation);
                Destroy(gameObject);
            }
            isDead = true;
        }

    }
}
