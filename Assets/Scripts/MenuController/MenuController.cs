using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //[Header("Player Movement")]

    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject DialogNoGameSaved = null;
    [SerializeField] private GameObject MainMenuContainer = null;
    [SerializeField] private GameObject DialogExitGame = null;


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("OfficeSprint"))
        {
            levelToLoad = PlayerPrefs.GetString("OfficeSprint");
            // To create a level to push into this filed SavedLevel
            //PlayerPrefs.setString("SavedLevel", yourlevel);
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            // Controls the no saved game dialog appearing
            DialogNoGameSaved.SetActive(true);
        }
    }

    public void ExitButtonYes()
    {
        Application.Quit();
    }

    public void ExitButtonNo()
    {
        MainMenuContainer.SetActive(true);
        DialogExitGame.SetActive(false);

    }
}
