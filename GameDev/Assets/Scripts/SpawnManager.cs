using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public float spawnTime = 1f;
    public int nightNumber = 1;
    public GameObject cheese;
    public GameObject joghurt;
    public GameObject milk;
    public GameObject mozzarella;
    public GameObject tree;
    public GameObject rock;
    public GameObject[] spawnpoints;
    public Text nightNumberText;
    public Transform[] treeArray;
    public Transform[] rockArray;

    private PlayerHealth playerHealth;
    private Transform sunPosition;
    private bool isNight = false;
    private int shootable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject sun = GameObject.Find("Sun");
        playerHealth = player.GetComponent<PlayerHealth>();
        sunPosition = sun.transform;
        shootable = LayerMask.GetMask("Shootable");
    }

    private void Update()
    {
        if (sunPosition.position.y <= 0 && !isNight) {
            isNight = true;
            startWave(nightNumber);
            nightNumber++;
            nightNumberText.text = "Night " + (nightNumber - 1) ;
        }
        else if (sunPosition.position.y > 0 && isNight) {
            isNight = false;
            ReplaceResources();
        }
    }

    void ReplaceResources() {
        for (int i = 0; i < treeArray.Length; i++) {
            Collider[] intersection = Physics.OverlapSphere(treeArray[i].position, .2f, shootable);
            if (intersection.Length == 0) {
                Instantiate(tree, treeArray[i].position, Quaternion.identity);
            }
        }
        for (int i = 0; i < rockArray.Length; i++) {
            Collider[] intersection = Physics.OverlapSphere(rockArray[i].position, 1f, shootable);
            if (intersection.Length == 0) {
                Instantiate(rock, rockArray[i].position, Quaternion.identity);
            }
        }
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
