using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;

    private PlayerHealth playerHealth;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Spawn()
    {
        if (playerHealth.currentHealth > 0)
            Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
    }
}
