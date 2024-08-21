using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunthersOfficeWinState : MonoBehaviour
{
    private Countdown countdown;

    void Start()
    {
        // Find the Countdown script in the scene
        countdown = FindObjectOfType<Countdown>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the collider
        if (other.CompareTag("Player") && countdown != null)
        {
            // Log the current time and update the scoreboard
            float finalTime = countdown.CurrentTime;

            // Update the scoreboard via the Countdown script
            countdown.UpdateScoreboard(finalTime);

            // Trigger any win state logic here, e.g., show a win screen
            Debug.Log("Player has won! Time: " + finalTime);
        }
    }
}