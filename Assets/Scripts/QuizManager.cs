using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// using Game.FinalCharacterController;

public class QuizManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject welcomePanel;
    public GameObject quizPanel;
    public GameObject resultsPanel;

    [Header("UI Elements")]
    public Text welcomeText;
    public Text questionText;
    public Text feedbackText;
    public Text resultText;

    public Button startQuizButton;
    public Button closeWelcomeButton;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button closeQuizButton;
    public Button restartButton;

    // References to Player's Movement and Animation components
    public GameObject player;  // Reference to the player (can be assigned in Inspector)
    private PlayerController playerController;

    private int correctAnswerIndex;
    private int score = 0;
    private int currentQuestionIndex = 0;

    private struct Question
    {
        public string question;
        public string[] options;
        public int correctAnswer;
    }

    private Question[] questions = new Question[]
    {
        new Question { question = "A customer complains about a defective product. What is the best response?", 
            options = new string[] { "Ignore it", "Offer a replacement/refund", "Blame the manufacturer" }, 
            correctAnswer = 1 },

        new Question { question = "Which financial statement shows a company's profits and losses?", 
            options = new string[] { "Balance Sheet", "Income Statement", "Cash Flow Statement" }, 
            correctAnswer = 1 },

        new Question { question = "What does ROI stand for in business?", 
            options = new string[] { "Rate of Interest", "Return on Investment", "Revenue of Industry" }, 
            correctAnswer = 1 },

        new Question { question = "An employee is constantly late. How should a manager handle this?", 
            options = new string[] { "Fire them immediately", "Discuss the issue privately", "Ignore the issue" }, 
            correctAnswer = 1 }
    };

    void Start()
    {
        // Initialize references
        playerController = player.GetComponent<PlayerController>();

        // Hide quiz and result panels initially
        welcomePanel.SetActive(false);
        quizPanel.SetActive(false);
        resultsPanel.SetActive(false);
        feedbackText.gameObject.SetActive(false);

        // Set welcome message
        welcomeText.text = "Welcome to the Odette School of Business!\n\n" +
                           "Odette School of Business prepares students for leadership roles in the corporate world. " +
                           "Take the quiz to see if you have the aptitude for business studies!";
        
        // Button Listeners
        startQuizButton.onClick.AddListener(StartQuiz);
        closeWelcomeButton.onClick.AddListener(CloseWelcome);
        closeQuizButton.onClick.AddListener(CloseQuiz);
        restartButton.onClick.AddListener(RestartQuiz);

        // Answer buttons
        option1Button.onClick.AddListener(() => CheckAnswer(0));
        option2Button.onClick.AddListener(() => CheckAnswer(1));
        option3Button.onClick.AddListener(() => CheckAnswer(2));
    }

    public void OpenWelcomePanel()
    {
        welcomePanel.SetActive(true);
        // Unlock the cursor when quiz UI is active
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void CloseWelcome()
    {
        welcomePanel.SetActive(false);
        // Lock the cursor back if the quiz has not started
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void StartQuiz()
    {
        // Disable player movement and animation while the quiz is active
        playerController.enabled = false;
        welcomePanel.SetActive(false);
        quizPanel.SetActive(true);
        currentQuestionIndex = 0;
        score = 0;
        ShowQuestion(currentQuestionIndex);

        // Ensure the cursor remains visible while taking the quiz
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ShowQuestion(int index)
    {
        questionText.text = questions[index].question;
        option1Button.GetComponentInChildren<Text>().text = questions[index].options[0];
        option2Button.GetComponentInChildren<Text>().text = questions[index].options[1];
        option3Button.GetComponentInChildren<Text>().text = questions[index].options[2];
        correctAnswerIndex = questions[index].correctAnswer;
    }

    private void CheckAnswer(int selectedIndex)
    {
        bool isCorrect = selectedIndex == correctAnswerIndex;
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = isCorrect ? "Correct!" : "Wrong answer.";
        feedbackText.color = isCorrect ? Color.green : Color.red;

        if (isCorrect) score++;

        // Disable buttons to prevent multiple clicks
        option1Button.interactable = false;
        option2Button.interactable = false;
        option3Button.interactable = false;

        StartCoroutine(NextQuestionDelay());
    }

    IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(1.5f);
        feedbackText.gameObject.SetActive(false);

        // Enable buttons for next question
        option1Button.interactable = true;
        option2Button.interactable = true;
        option3Button.interactable = true;

        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            ShowQuestion(currentQuestionIndex);
        }
        else
        {
            ShowResults();
        }
    }

    private void ShowResults()
    {
        quizPanel.SetActive(false);
        resultsPanel.SetActive(true);

        string message = "Your Score: " + score + "/" + questions.Length + "\n";
        if (score == questions.Length)
        {
            message += "Excellent! You have great business acumen.";
        }
        else if (score >= questions.Length / 2)
        {
            message += "Good job! You have a solid understanding of business.";
        }
        else
        {
            message += "You might want to explore more about business concepts.";
        }

        resultText.text = message;
    }

    private void RestartQuiz()
    {
        resultsPanel.SetActive(false);
        // Re-enable player movement and animation
        playerController.enabled = true;
        StartQuiz();
    }

    private void CloseQuiz()
    {
        quizPanel.SetActive(false);
        resultsPanel.SetActive(false);
        // Re-enable player movement and animation
        playerController.enabled = true;

        // Lock and hide the cursor again when quiz is closed
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
