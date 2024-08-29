using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;    
    // Starting time in seconds
    [SerializeField] public float startTime = 300f; 
    public float currentTime;
    //Public Property for read-only access to the currentTime
    public float CurrentTime => currentTime;

    public delegate void TimeExpiredHandler();
    // Creates an event
    public event TimeExpiredHandler OnTimeExpired;

    [SerializeField] private AudioSource backgroundMusic;  

    // The following code was written by Roshan
    [SerializeField] private GameObject instructionPanel;
    [SerializeField] private GameObject checkpointPanel;
    [SerializeField] private TextMeshProUGUI checkpointText;

    private bool timerRunning = false;
    public bool gamePaused = true;
    // End of code written by Roshan


    void Start()
    {
        // Initializes current time
        currentTime = startTime;

        instructionPanel.SetActive(true); // Written by Roshan

        // Start playing background music if not already playing
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Pause();
        }
    }

    void Update()
    {
        // The following code was written by Roshan
        // Start countdown when enter is pressed
        if (!timerRunning && Input.GetKeyDown(KeyCode.Return))
        {
            StartCountdown();
        }
        // End of code written by Roshan
        if (timerRunning && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText();

            if(currentTime < 60)
            {
                countdownText.color = Color.red;
            }
        }
        // Stop timer value from reducing 
        else if (!timerRunning) // Added by Roshan
        {
            currentTime += 0;
            UpdateCountdownText();
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

    // The following code was written by Roshan
    // Update the flags and stop the music
    public void StopCountdown()
    {
        timerRunning = false;
        gamePaused = true;
        // Stop music
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
    }

    // Game Start - Remove the instruction panel, start the timer and music
    public void StartCountdown()
    {
        // Remove starting instruction panel
        instructionPanel.SetActive(false);
        timerRunning = true;
        gamePaused = false;
        // Play music
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();  
        }
    }

    // Show message at the checkpoint, pause the music and countdown
    public void PauseAtCheckpoint(string checkpointText1)
    {
        //  Update the flags
        timerRunning = false;
        gamePaused = true;
        // Show checkpoint panel and text
        checkpointPanel.SetActive(true);
        checkpointText.text = checkpointText1;
        // Pause music
        if (backgroundMusic != null)
        {
            backgroundMusic.Pause();
        }
    }

    // Mid Game - Remove message at the checkpoint, resume music and countdown
    public void ResumeFromCheckpoint()
    {
        // Remove the checkpoint panel
        checkpointPanel.SetActive(false);
        timerRunning = true;
        gamePaused = false;
        // Play music
        if (backgroundMusic != null)
        {
            backgroundMusic.Play();
        }
    }

    // Mid Game - Stop countdown and show checkpoint panel
    public void MidGamePause(GameObject checkpointPanel)
    {
        StopCountdown(); // Currently does not seem to work. To debug
        checkpointPanel.SetActive(true);
    }
    // End of code written by Roshan
}
