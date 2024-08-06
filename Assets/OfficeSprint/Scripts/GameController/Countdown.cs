using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    // Starting time in seconds
    [SerializeField] private float startTime = 300f; 
    private float currentTime;
    //Public Property for read-only access to the currentTime
    public float CurrentTime => currentTime;

    public delegate void TimeExpiredHandler();
    // Creates an event
    public event TimeExpiredHandler OnTimeExpired;



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
}
