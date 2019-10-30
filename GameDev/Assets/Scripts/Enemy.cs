using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int damage;
    public float timeBetweenAttacks = 0.5f;

    private GameObject player;
    private NavMeshAgent nav;
    private damage enemyHealth;
    private PlayerHealth playerHealth;
    private float timer;
    private bool playerInRange;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHealth = GetComponent<damage>();
        playerHealth = player.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            Attack();

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(player.transform.position);
        }
        else nav.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
            playerHealth.TakeDamage(damage);
    }
}
