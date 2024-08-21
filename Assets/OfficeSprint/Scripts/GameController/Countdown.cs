using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private TextMeshProUGUI scoreboardText; // Reference to the Scoreboard Text
    // Starting time in seconds
    [SerializeField] private float startTime = 300f; 
    private float currentTime;
    //Public Property for read-only access to the currentTime
    public float CurrentTime => currentTime;

    public delegate void TimeExpiredHandler();
    // Creates an event
    public event TimeExpiredHandler OnTimeExpired;

    private float shortestTimeToWin;

    [Header("Audio Components")]
    [SerializeField] private AudioSource backgroundMusic; // Reference to the AudioSource component for background music


    void Start()
    {
        // Initializes current time
        currentTime = startTime;

        // Start playing background music if not already playing
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.loop = true; // Ensure the music loops
            backgroundMusic.Play();
        }
        // Load the shortest time from PlayerPrefs
        shortestTimeToWin = PlayerPrefs.GetFloat("ShortestTimeToWin", float.MaxValue);
        UpdateScoreboardText();
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText();

            if(currentTime < 60)
            {
                countdownText.color = Color.red;
            }
        }
        else
        {
            currentTime = 0;
            // Trigger the event when timer reaches 0
            OnTimeExpired?.Invoke();

            // Stop the background music (optional)
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
            }
        }
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateScoreboardText()
    {
        if (shortestTimeToWin != float.MaxValue)
        {
            int minutes = Mathf.FloorToInt(shortestTimeToWin / 60);
            int seconds = Mathf.FloorToInt(shortestTimeToWin % 60);
            scoreboardText.text = $"Best Time: {minutes:00}:{seconds:00}";
        }
        else
        {
            scoreboardText.text = "Best Time: --:--";
        }
    }

    // Public method to update the scoreboard when called by the win state script
    public void UpdateScoreboard(float finalTime)
    {
        if (finalTime < shortestTimeToWin)
        {
            shortestTimeToWin = finalTime;
            PlayerPrefs.SetFloat("ShortestTimeToWin", shortestTimeToWin);
            PlayerPrefs.Save();
            UpdateScoreboardText();
        }
    }

}
