// This script was added by Roshan
// Plays sound, display the mid game story panel and resumes game when enter is pressed
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameCheckpoint : MonoBehaviour
{
    // Get the countdown 
    [SerializeField] public Countdown countdown;
    [SerializeField] private GameObject midGamePanel; 
    private AudioSource audioSource;
    // Default game state is not paused
    private bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Hide the mid game panel
        midGamePanel.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Show the mid game panel when Gunther collides
        if (other.CompareTag("Player"))
        {
            // Plays the sound
            audioSource.Play();
            // Show the mid game panel
            midGamePanel.SetActive(true);
            // Stop timer 
            countdown.StopCountdown();
            // Game state set to pause
            gamePaused = true;
        }
    }


    void Update()
    {
        // Resume game when enter is pressed
        if (gamePaused && Input.GetKeyDown(KeyCode.Return))
        {
            // Hide the game panel
            midGamePanel.SetActive(false);
            // Resume the countdown
            countdown.ResumeFromCheckpoint();
            // Update game state
            gamePaused = false;
        }
    }
}
