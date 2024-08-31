// This script was added by Roshan
// Handle whe win state
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinState : MonoBehaviour
{
    // Text when Gunther Wins
    public TMP_Text winText;
    // Get the countdown
    public Countdown countdown;
    // Total game time
    private float totalTime = 300f;
    // Background music when Gunther reaches office!
    public AudioSource audioSource;
    // Store the position and rotation of Gunther
    private Vector3 winPosition = new Vector3(323f, 138f, 146.3f);
    private Quaternion winRotation = Quaternion.Euler(0f, -80f, 0f);
    // Character controller component
    public CharacterController controller;

    void Start()
    {
        // Clear the win text
        winText.text = "";
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();
        if (playerScript != null)
        {
            controller.enabled = false;
            // Updates the checkpoint on PlayerScript.cs (script from checkpoint.cs)
            playerScript.UpdateLatestCheckpoint(winPosition);
            // Put Gunther on top of the office building
            playerScript.transform.position = winPosition;
            // Rotate facing the city
            playerScript.transform.rotation = winRotation;
            controller.enabled = true;
        }

        // Stop countdown and show win text when Gunther collides with the final bottom support
        if (other.CompareTag("Player"))
        {
            // Stop the countdown
            countdown.StopCountdown();
            // Show the congratulations text
            DisplayWinText();
            audioSource.Play();
        }
    }

    public void DisplayWinText()
    {
        // Count the duration of the game played
        float timeTaken = totalTime - countdown.CurrentTime;
        // Get the minutes
        int min = Mathf.FloorToInt(timeTaken / 60);
        // Get the seconds from remainder
        int sec = Mathf.FloorToInt(timeTaken % 60);
        // Display the text and time taken
        winText.text = "Congrats! You reached Office in " + min + " minute & " + sec + " seconds! Press Escape to view the Menu.";
    }
}
