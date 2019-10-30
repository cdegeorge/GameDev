using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 3f;

    private PlayerHealth playerHealth;
    private Transform sunPosition;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject sun = GameObject.Find("Sun");
        playerHealth = player.GetComponent<PlayerHealth>();
        sunPosition = sun.transform;
    }

    private void Spawn()
    {
        if (playerHealth.currentHealth > 0 && sunPosition.position.y <= 0)
            Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
    }
}
