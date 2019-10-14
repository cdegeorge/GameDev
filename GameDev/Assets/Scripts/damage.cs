using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public GameObject stickPile;

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
            if (gameObject.CompareTag("Tree")) Instantiate(stickPile, new Vector3(position.x, -5, position.z), gameObject.transform.rotation);
            isDead = true;
        }

    }
}
