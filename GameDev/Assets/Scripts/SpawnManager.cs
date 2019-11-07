using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spawnTime = 1f;
    public GameObject cheese;
    public GameObject joghurt;
    public GameObject milk;
    public GameObject mozzarella;
    public GameObject[] spawnpoints;

    private PlayerHealth playerHealth;
    private Transform sunPosition;
    private int nightNumber = 1;
    private bool isNight = false;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject sun = GameObject.Find("Sun");
        playerHealth = player.GetComponent<PlayerHealth>();
        sunPosition = sun.transform;
    }

    private void Update()
    {
        if (sunPosition.position.y <= 0 && !isNight)
        {
            isNight = true;
            startWave(nightNumber);
            nightNumber++;
        }
        else if (sunPosition.position.y > 0 && isNight)
            isNight = false;
    }

    private void startWave(int waveNumber)
    {
        StartCoroutine(Spawn(joghurt, waveNumber * 2));
        StartCoroutine(Spawn(mozzarella, waveNumber * 2));
        if (waveNumber % 2 == 0)
            StartCoroutine(Spawn(cheese, waveNumber / 2));
        if (waveNumber % 3 == 0)
            StartCoroutine(Spawn(milk, waveNumber / 3));
    }

    private IEnumerator Spawn(GameObject enemy, int times)
    {
        int count = 1;
        while (count <= times)
        {
            int randNum = Random.Range(0, 3);
            GameObject spawnpoint = spawnpoints[randNum];

            if (playerHealth.currentHealth > 0 && isNight)
                Instantiate(enemy, spawnpoint.transform.position, Quaternion.identity);

            count++;
            yield return new WaitForSeconds(spawnTime);
        }
        yield return 0;
    }
}
