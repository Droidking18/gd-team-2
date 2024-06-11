using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    // Keeps track if game is paused
    public static bool gameIsPaused = false;

    // PauseMenu Panel UI
    public GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

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
    //Saves current game and loads MainMenu scene
    public void loadMenu()
    {
        // Saves current scene using PlayerPrefs (by name)
        string currentSceneName = SceneManager.GetActiveScene().name;

        //Get the player game object and its position 
        GameObject player = GameObject.Find("Player");
        Vector3 playerPosition = player.transform.position;

        PlayerPrefs.SetString("SavedGame", currentSceneName);

        //Save the player position
        PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
        PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
        PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

        Debug.Log("Scene saved: " + currentSceneName);

        // Loads MainMenu scene either way
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;    
    }

    public void quitGame()
    {
        Debug.Log("Quit Game has been pressed");
        Application.Quit();
    }
}
