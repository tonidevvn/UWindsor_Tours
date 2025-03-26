using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    private Toggle fullscreenToggle;
    private DropdownField graphicsDropdown;
    private Slider musicSlider;
    private Slider sfxSlider;
    private Button resetButton;
    private Button backButton;

    private AudioManager audioManager;

    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        fullscreenToggle = root.Q<Toggle>("fullscreen-toggle");
        graphicsDropdown = root.Q<DropdownField>("graphics-dropdown");
        musicSlider = root.Q<Slider>("music-slider");
        sfxSlider = root.Q<Slider>("sfx-slider");
        resetButton = root.Q<Button>("reset-btn");
        backButton = root.Q<Button>("back-btn");
        

        // Event listeners
        fullscreenToggle.RegisterValueChangedCallback(evt =>
        {
            Screen.fullScreen = evt.newValue;
            PlayerPrefs.SetInt("fullscreen", evt.newValue ? 1 : 0); // save to prefs
            PlayerPrefs.Save();
        });

        graphicsDropdown.RegisterValueChangedCallback(evt =>
        {
            QualitySettings.SetQualityLevel(graphicsDropdown.index);
            PlayerPrefs.SetInt("quality", graphicsDropdown.index); // save to prefs
            PlayerPrefs.Save();
        });

        musicSlider.RegisterValueChangedCallback(evt => {
            audioManager.SetMusicVolume(evt.newValue);
            audioManager.SaveVolumeSettings(); // persist
            Debug.Log($"Music volume set to {evt.newValue}");
        });

        sfxSlider.RegisterValueChangedCallback(evt => {
            audioManager.SetSFXVolume(evt.newValue);
            audioManager.SaveVolumeSettings(); // persist
            Debug.Log($"SFX volume set to {evt.newValue}");
        });

        resetButton.clicked += ResetSettings;
        backButton.clicked += ReturnToMainMenu;
    }

    void Start()
    {
        // Load saved values (or use default if none)
        bool isFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        int maxQuality = QualitySettings.names.Length - 1;
        int qualityLevel = Mathf.Clamp(PlayerPrefs.GetInt("quality", 2), 0, maxQuality);

        Screen.fullScreen = isFullscreen;
        QualitySettings.SetQualityLevel(qualityLevel);

        // Apply to UI
        fullscreenToggle.value = isFullscreen;

        // Initialize graphics dropdown options (Low, Medium, High)
        graphicsDropdown.choices = new List<string>(QualitySettings.names);
        graphicsDropdown.value = QualitySettings.names[qualityLevel];

        musicSlider.value = audioManager.GetMusicVolume();
        sfxSlider.value = audioManager.GetSFXVolume();
        
    }

    private void ResetSettings()
    {
        audioManager.PlayMouseClick();
        fullscreenToggle.value = true;
        graphicsDropdown.value = "Medium";
        musicSlider.value = 0.5f;
        sfxSlider.value = 0.5f;

        Screen.fullScreen = true;
        QualitySettings.SetQualityLevel(1);
        audioManager.SetMusicVolume(0.5f);
        audioManager.SetSFXVolume(0.5f);
    }

    private void ReturnToMainMenu()
    {
        audioManager.PlayMouseClick();
        Loader.Load("MainMenu");
    }
}
