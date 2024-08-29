// This script was added by Roshan
// Handle whe win state
using UnityEngine;
using UnityEngine.UI;

public class WinState : MonoBehaviour
{
    // Text when Gunther Wins
    public Text winText;
    // Get the countdown
    public Countdown countdown;
    // Total game time
    private float totalTime = 300f;
    // Background music when Gunther reaches office!
    public AudioSource audioSource;

    void Start()
    {
        // Clear the win text
        winText.text = "";
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Stop countdown and show win text when Gunther collides with the final bottom support
        if (other.CompareTag("Player"))
        {
            // Stop the countdown
            countdown.StopCountdown();
            // Show the congratulations text
            DisplayWinText();
            audioSource.Play();
            countdown.StopCountdown();
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
        winText.text = "Congrats! You reached Office in " + min + "minutes & " + sec + "seconds! Press Excape to view the Menu.";
    }
}
