// Handle whe win state. Stop countdown when Gunther reaches the Office tower, puts him on
// top of the Office building and display the wintext
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinState : MonoBehaviour
{
    public TMP_Text winText;
    public Countdown countdown;
    // Total game time
    private float totalTime = 300f;
    public AudioSource audioSource;
    // Store the position and rotation of Gunther
    private Vector3 winPosition = new Vector3(323f, 138f, 146.3f);
    private Quaternion winRotation = Quaternion.Euler(0f, -80f, 0f);
    public CharacterController controller;

    void Start()
    {
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
            countdown.StopCountdown();
            // Show the congratulations text and time it took to finish the game
            DisplayWinText();
            audioSource.Play();
        }
    }

    public void DisplayWinText()
    {
        // Count the duration of the game played
        float timeTaken = totalTime - countdown.CurrentTime;
        int min = Mathf.FloorToInt(timeTaken / 60);
        int sec = Mathf.FloorToInt(timeTaken % 60);
        // Display the time taken to finish the game
        winText.text = "Congrats! You reached Office in " + min + " minute & " + sec + " seconds! Press Escape to view the Menu.";
    }
}