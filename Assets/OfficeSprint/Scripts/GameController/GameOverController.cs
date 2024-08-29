using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [Header("Music")] public AudioSource gameOverMusic;
    [SerializeField] private GameObject DialogExitGame = null;
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;


    // Start is called before the first frame update
    void Start()
    {
        if (gameOverMusic != null)
        {
            Debug.Log("AudioSource is assigned");
            gameOverMusic.Play();
            Debug.Log("AudioSource is playing: " + gameOverMusic.isPlaying);
        }
        else
        {
            Debug.LogError("AudioSource not assigned in GameOverController!");
        }
        if (gameOverMusic != null)
        {
            // Start playing the music
            gameOverMusic.Play();
        }

    }

    public void RestartGameDialogYes()
    {
        Debug.Log("Restart game button pressed");
        // Loads the game scene (OfficeSprint)
        SceneManager.LoadScene("OfficeSprint");
    }

    public void ExitButtonYes()
    {
        Debug.Log("Exit game confirmed");
        Application.Quit();
    }

    public void ExitButtonNo()
    {
        // Hides the exit dialog box
        DialogExitGame.SetActive(false);

    }

    // Confirmation that new volume settings have been applied 
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);

    }
}
