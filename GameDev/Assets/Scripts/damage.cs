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
            Destroy(gameObject);
            Vector3 position = gameObject.transform.position;
            Debug.Log(gameObject.name);
            if (gameObject.CompareTag("Tree")) Instantiate(stickPile, new Vector3(position.x, position.y, position.z), gameObject.transform.rotation);
            else if (gameObject.CompareTag("Rock")) Instantiate(rockPile, new Vector3(position.x, position.y, position.z), gameObject.transform.rotation);
            isDead = true;
        }

    }
}
