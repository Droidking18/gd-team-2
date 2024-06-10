using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Gameplay settings")]
    [SerializeField] private TMP_Text controllerSensitivityTextValue = null;
    [SerializeField] private Slider controllerSensitivitySlider = null;
    [SerializeField] private int defaultSensitivity = 4;
    public int mainControllerSensitivity = 4;

    [Header("Toggle settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;


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
        // Shows the menu container 
        MainMenuContainer.SetActive(true);
        // Hides the exit dialog box
        DialogExitGame.SetActive(false);

    }

    // Method that controls the volume  
    public void SetVolume(float volume)
    {
        //Changes all the audio in the game (0, 1)
        AudioListener.volume = volume;
        // Update the volume value text
        volumeTextValue.text = volume.ToString("0.0");
    }

    //Applies chosen volume
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        //Show prompt
        StartCoroutine(ConfirmationBox());

    }

    public void SetControllerSensitivity(float sensitivity)
    {
        mainControllerSensitivity = Mathf.RoundToInt(sensitivity);
        controllerSensitivityTextValue.text = sensitivity.ToString("0");

    }
    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInverter", 1);
            

        }
        else
        {
            PlayerPrefs.SetInt("masterInverter", 0);
            // Not invert Y
        }
        PlayerPrefs.SetFloat("masterSensitivity", mainControllerSensitivity);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");

    }
    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;

    }
    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;

    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        //Change brightness with post processing or something else

        //Here we can set the quality level based on the settings
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        // Changes the quality setting to the index we changed it to
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

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
        if(MenuType == "Gameplay")
        {
            controllerSensitivityTextValue.text = defaultSensitivity.ToString("0");
            controllerSensitivitySlider.value = defaultSensitivity;
            mainControllerSensitivity = defaultSensitivity;
            invertYToggle.isOn = false;
            GameplayApply();

        }
        if (MenuType == "Graphics")
        {
            //reset brightness value
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

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



