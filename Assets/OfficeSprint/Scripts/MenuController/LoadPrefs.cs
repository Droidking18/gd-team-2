using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private bool allowed = false;
    [SerializeField] private MenuController menuController;

    [Header("Volume settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Quality level settings")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen settings")]
    [SerializeField] private Toggle fullScreenToggle;

    private void Awake()
    {
        if (allowed)
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

    }
}
