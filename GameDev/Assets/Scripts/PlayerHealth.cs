﻿using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                           
    public int currentHealth;
    public GameObject gameOverScreen;
    public Slider healthSlider;                                 
    public Image damageImage;                                   
    //public AudioClip deathClip;                                 
    public float flashSpeed = 5f;                               
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     


    Animator anim;                                              
    AudioSource playerAudio;                                    
    PlayerMovement playerMovement;                        
    bool isDead;                                               
    bool damaged;                                               

    void Awake() {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        currentHealth = startingHealth;
    }

    void Update() {
        if (damaged) {
            damageImage.color = flashColour;
        }
        else {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void TakeDamage(int amount) {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead) {
            isDead = true;
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
    }    
}

