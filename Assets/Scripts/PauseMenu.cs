using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    private VisualElement pauseMenu;

    public QuizManager quizManagerObject;

    bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        pauseMenu = root.Q<VisualElement>("pause-menu");
        root.Q<Button>("resume-btn").clicked += () => ResumeGame();
        root.Q<Button>("back-to-main-menu-btn").clicked += () => BackToMainMenu();
        
        isPaused = false;
        pauseMenu.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    private void ResumeGame()
    {
        isPaused = false;
        pauseMenu.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        isPaused = true;
        pauseMenu.style.display = DisplayStyle.Flex;
        Time.timeScale = 0;
    }

    private void BackToMainMenu()
    {
        Loader.LoadMenu();
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
