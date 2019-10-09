using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public float health;
    public float currentHealth;
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
            isDead = true;
        }

    }
}
