using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    public float health;
    public float currentHealth;
    public GameObject stickPile;
    public GameObject rockPile;
    public Transform player;

    private bool isDead = false;

    AudioSource audio;

    void Awake() {
        currentHealth = health;
        audio = GetComponent<AudioSource>();
    }

    
    public void TakeDamage(float amount) {
        if(isDead) {
            return;
        }
        currentHealth -= amount;
        if (audio != null)
            audio.Play();

        if (currentHealth <= 0) {

            if (gameObject.CompareTag("Tree"))
            {
                Vector3 position = new Vector3(player.position.x, player.position.y, player.position.z > 0 ? player.position.z + 1 : player.position.z - 1);
                Instantiate(stickPile, position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Rock")) 
            {
                Vector3 position = player.position;
                Debug.Log("Player Position: " + player.position);
                Debug.Log("Rock Position: " + position);
                Instantiate(rockPile, position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Enemy")) {
                Destroy(gameObject);
            }
            isDead = true;
        }

    }
}
