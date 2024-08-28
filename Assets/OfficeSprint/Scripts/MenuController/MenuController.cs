using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Continue")]
    private int sceneToContinue;

    [Header("Volume settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;
    [Header("Music")] public AudioSource menuMusic;
    [SerializeField] private AudioListener audioListener;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;

    [Header("Levels to load")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject DialogNoGameSaved = null;
    [SerializeField] private GameObject MainMenuContainer = null;
    [SerializeField] private GameObject DialogExitGame = null;

    [Header("Resolutions dropdown")]
    public TMP_Dropdown resolutionDropdow;
    private Resolution[] resolutions;

    private void Start()
    {
        if (menuMusic != null)
        {
            // Start playing the music
            menuMusic.Play(); 
        }
        resolutions = Screen.resolutions;
        resolutionDropdow.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdow.AddOptions(options);
        resolutionDropdow.value = currentResolutionIndex;
        resolutionDropdow.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void ContinueDialogYes()
    {

        sceneToContinue = PlayerPrefs.GetInt("SavedGame");

        if(sceneToContinue >= 0)
        {
            SceneManager.LoadScene(sceneToContinue);
        }
        else
        {
            Debug.Log("No saved scene");
            // Controls the no saved game dialog appearing
            DialogNoGameSaved.SetActive(true);
        }
    }

    public void NewGameDialogYes()
    {
        // Loads the game scene (OfficeSprint)
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("OfficeSprint"))
        {
            levelToLoad = PlayerPrefs.GetString("OfficeSprint");
            // To create a level to push into this filed SavedLevel
            //PlayerPrefs.setString("SavedGame", yourlevel);
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
        Debug.Log("Exit game confirmed");
        Application.Quit();
    }

    public void ExitButtonNo()
    {
        // Shows the menu container 
        MainMenuContainer.SetActive(true);
        // Hides the exit dialog box
        DialogExitGame.SetActive(false);

    }

    // Method that controls the volume  
    public void SetVolume(float volume)
    {
        // Check if AudioListener was found
        if (audioListener != null)
        {
            AudioListener.volume = volume;
            volumeTextValue.text = volume.ToString("0.0");
        }
    }

    //Applies chosen volume
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        //Show prompt
        StartCoroutine(ConfirmationBox());

    }
    // Sets quality level from dropdown options (Low,Medium,High, Ultra)
    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
        Debug.Log("_qualityLevel: " + _qualityLevel);
        Debug.Log("qualityIndex: " + qualityIndex);

    }
    public void GraphicsApply()
    {
        //Here we can set the quality level based on the settings
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        // Changes the quality setting to the index we changed it to
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = fullScreenToggle.isOn;

        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string MenuType)
    {
        if(MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (MenuType == "Graphics")
        {
            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(2);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdow.value = resolutions.Length;
            GraphicsApply();

        }

    }

    // Confirmation that new volume settings have been applied 
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);

    }

}



