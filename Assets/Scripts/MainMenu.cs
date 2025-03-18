using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu: MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("start-btn").clicked += () => StartGame();
        root.Q<Button>("settings-btn").clicked += () => OpenSettings();
        root.Q<Button>("quit-btn").clicked += () => QuitGame(); 
    }

    private void StartGame() {
        Loader.Load("Game");
    }

    private void OpenSettings() {
        Debug.Log("Settings opened!");
    }

    private void QuitGame() {
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
