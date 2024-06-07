using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;

    [Header("Quality level settings")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen settings")]
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity settings")]
    [SerializeField] private TMP_Text controllerSensitivityTextValue = null;
    [SerializeField] private Slider controllerSensitivitySlider = null;

    [Header("Invert Y settings")]
    [SerializeField] private Toggle invertYToggle = null;

    private void Awake()
    {
        if (canUse)
        {
            if (PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
        }
        else
        {
            menuController.ResetButton("Audio");
        }

        if (PlayerPrefs.HasKey("masterQuality"))
        {
            int localQuality = PlayerPrefs.GetInt("masterQuality");
            qualityDropdown.value = localQuality;
            QualitySettings.SetQualityLevel(localQuality);
        }

        if (PlayerPrefs.HasKey("masterFullscreen"))
        {
            int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");

            if(localFullscreen == 1)
            {
                Screen.fullScreen = true;
                fullScreenToggle.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                fullScreenToggle.isOn = false;
            }
        }
        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetInt("masterBrightness");

            brightnessTextValue.text = localBrightness.ToString("0.0") ;
            brightnessSlider.value = localBrightness;
            //change the brightness
        }

        if (PlayerPrefs.HasKey("masterSensitivity"))
        {
            float localSensitivity = PlayerPrefs.GetInt("masterSensitivity");

            controllerSensitivityTextValue.text = localSensitivity.ToString("0");
            controllerSensitivitySlider.value = localSensitivity;
            menuController.mainControllerSensitivity = Mathf.RoundToInt(localSensitivity);
        }

        if (PlayerPrefs.HasKey("masterInverter"))
        {
            if (PlayerPrefs.GetInt("masterInverter") == 1)
            {
                invertYToggle.isOn = true;
            }
            else
            {
                invertYToggle.isOn = false;
            }
        }
    }
}
