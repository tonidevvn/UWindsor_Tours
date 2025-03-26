using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private VisualElement pauseMenu;

    private IngameSettingsMenu settingsMenu;

    public QuizManager quizManagerObject;

    bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        pauseMenu = root.Q<VisualElement>("pause-menu");
        settingsMenu = FindFirstObjectByType<IngameSettingsMenu>();

        root.Q<Button>("resume-btn").clicked += () => ResumeGame();
        root.Q<Button>("back-to-main-menu-btn").clicked += () => BackToMainMenu();
        root.Q<Button>("settings-btn").clicked += () => OpenSettings();
        

        isPaused = false;
        pauseMenu.style.display = DisplayStyle.None;
        
        if (settingsMenu != null)
            settingsMenu.Hide(); // ensure it's hidden at start
    
        Time.timeScale = 1;
    }
    
    private void OpenSettings()
    {
        Debug.Log("SettingsMenu " + settingsMenu.name);
        pauseMenu.style.display = DisplayStyle.None;
        if (settingsMenu != null) settingsMenu.Show();        
        Debug.Log("Settings opened (in-game)");
    }

    private void ResumeGame()
    {
        Debug.Log("Game Resumed");
        isPaused = false;
        pauseMenu.style.display = DisplayStyle.None;
        if (settingsMenu != null) settingsMenu.Hide();
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        Debug.Log("Game Paused");
        isPaused = true;
        pauseMenu.style.display = DisplayStyle.Flex;
        if (settingsMenu != null) settingsMenu.Hide();
        Time.timeScale = 0;
    }

    private void BackToMainMenu()
    {
        Loader.LoadMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (quizManagerObject.IsOpened())
            {
                quizManagerObject.ClosePanel();
            }
            else
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }
}
