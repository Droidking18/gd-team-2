using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Keeps track if game is paused
    public static bool gameIsPaused = false;

    //Reference to player script for save game function
    [Header("Player script")]
    [SerializeField] private Player player;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    // PauseMenu Panel UI
    public GameObject pauseMenuUI;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void PauseGame()
    {
        // Activates the PauseMenu Panel
        pauseMenuUI.SetActive(true);
        // Stops the game
        Time.timeScale = 0f;
        // Sets the bool gameIsPaused 
        gameIsPaused = true;
    }

    //Saves current player position and countdown timer
    public void SaveGame()
    {
        if (player != null) 
        {
            player.SavePlayerData(); 
            Debug.Log("Game Saved");
            // Confirmation message to the player
            StartCoroutine(ConfirmationBox());
        }
        else
        {
            Debug.LogError("Player not found. Could not save game.");
        }
    }
    //Loads MainMenu scene
    public void loadMenu()
    {
        // Loads MainMenu scene either way
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;    
    }

    public void quitGame()
    {
        try
        {
            SaveGame();
        }
        catch (Exception e)
        {
            Debug.LogError("Not possible to save, error: " + e);
        }
        Debug.Log("Quit Game has been pressed");
        Application.Quit();
    }

    // Confirmation that new volume settings have been applied 
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);

    }
}
