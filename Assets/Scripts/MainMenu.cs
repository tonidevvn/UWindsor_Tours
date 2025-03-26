using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu: MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("start-btn").clicked += () => StartGame();
        root.Q<Button>("settings-btn").clicked += () => OpenSettings();
        root.Q<Button>("quit-btn").clicked += () => QuitGame();
        root.Q<Button>("start-btn").RegisterCallback<MouseEnterEvent>(evt => audioManager.PlayMouseHover());
        root.Q<Button>("settings-btn").RegisterCallback<MouseEnterEvent>(evt => audioManager.PlayMouseHover());
        root.Q<Button>("quit-btn").RegisterCallback<MouseEnterEvent>(evt => audioManager.PlayMouseHover());
    }

    private void StartGame() {
        audioManager.PlayMouseClick();
        Loader.LoadGame();
    }

    private void OpenSettings() {
        audioManager.PlayMouseClick();
        Loader.LoadSettingsMenu();
    }

    private void QuitGame() {
        audioManager.PlayMouseClick();
        Debug.Log("Quit game!");
        // save any game data here
        #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
