using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject welcomePanel;
    public GameObject undergraduateProgramsPanel;
    public GameObject graduateProgramsPanel;
    public GameObject programPanel;
    public GameObject quizPanel;
    public GameObject resultsPanel;

    [Header("Welcome Panel UI")]
    public Text welcomeText;
    public Button undergraduateProgramsButton;
    public Button graduateProgramsButton;
    public Button closeWelcomeButton;

    [Header("Programs Panel UI")]
    public Transform undergraduateProgramsContent;
    public Transform graduateProgramsContent;
    public Button programButtonPrefab;

    [Header("Program Panel UI")]
    public Text programTitleText;
    public Text programContentText;
    public Button nextButton;
    public Button startQuizButton;
    public Button closeDetailsButton;

    [Header("Quiz Panel UI")]
    public Text questionText;
    public Button[] answerButtons;
    public Text feedbackText;

    [Header("Results Panel UI")]
    public Text resultText;
    public Button restartButton;
    public Button closeQuizButton;

    private List<ProgramInfo> undergraduatePrograms = new List<ProgramInfo>();
    private List<ProgramInfo> graduatePrograms = new List<ProgramInfo>();
    private ProgramInfo currentProgram;
    private int currentSlideIndex = 0;
    private int score = 0;
    private int currentQuestionIndex = 0;

    // References to Player's Movement and Animation components
    public GameObject player;  // Reference to the player (can be assigned in Inspector)
    private PlayerController playerController;
    public CoinCollider coinCollider;

    void Start()
    {
        InitializePrograms();
        SetupWelcomePanel();
        SetupProgramPanels();
        SetupQuizPanel();
        SetupResultsPanel();
        playerController = player.GetComponent<PlayerController>();
        HideAllPanels();
        if (coinCollider == null) // Assign only if not set in the Inspector
        {
            coinCollider = FindObjectOfType<CoinCollider>(); 
        }
    }

    public void OpenQuiz()
    {
        HideAllPanels();
        ShowWelcomePanel();
        playerController.enabled = false;
    }

    void InitializePrograms()
    {
        // Add undergraduate programs
        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Co-op Available</b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• Combined Honours Programs Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements (Canadian High School Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Advanced Functions (MHF4U)</i>, <i>Calculus & Vectors (MCV4U)</i>, or <i>Math of Data Management (MDM4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in at least one Grade 12 U math course</i>)\n" +
                "• <b>Co-op Minimum Average:</b> <color=green>78%</color>\n" +
                "• <b>Mean Average:</b> <color=blue>84.7%</color>",

                "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Grade 12 Advanced Functions</i>, <i>Grade 12 Calculus & Vectors</i>, or <i>Grade 12 Math of Data Management</i>. <b>English required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in at least one Grade 12 math course</i>)\n" +
                "• <b>Co-op Minimum Average:</b> <color=green>78%</color>\n" +
                "• <b>Mean Average:</b> <color=blue>84.7%</color>"
            },
            new Question[] {
                new Question("<b>What is the minimum average required for the Business Administration program?</b>",
                    new string[] {"70%", "73%", "78%"}, 1),
                new Question("<b>Is a thesis option available in this program?</b>",
                    new string[] {"Yes", "No", "Only for Co-op students"}, 0),
                new Question("<b>What is the mean average of admitted students?</b>",
                    new string[] {"73%", "78%", "84.7%"}, 2)
            }
        ));

        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration and Computer Science",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Co-op Available</b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements (Canadian High School Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> <i>Advanced Functions (MHF4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Strongly Recommended:</b> <i>Calculus & Vectors (MCV4U)</i>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% average of all math courses with at least one Grade 12 U math course or equivalent at 70%</i>)\n" +
                "• <b>Co-op Minimum Average:</b> <color=green>78%</color>\n" +
                "• <b>Mean Average:</b> <color=blue>82%</color>",

                "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> <i>Grade 12 Advanced Functions</i>. <b>Grade 12 English required.</b>\n" +
                "• <b>Strongly Recommended:</b> <i>Grade 12 Calculus & Vectors</i>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% average of all math courses with at least one Grade 12 math course or equivalent at 70%</i>)\n" +
                "• <b>Co-op Minimum Average:</b> <color=green>78%</color>\n" +
                "• <b>Mean Average:</b> <color=blue>82%</color>"
            },
            new Question[] {
                new Question("<b>What course is strongly recommended for this program?</b>",
                    new string[] {"Advanced Functions", "Calculus & Vectors", "Data Management"}, 1),
                new Question("<b>What is the minimum average required for Co-op in this program?</b>",
                    new string[] {"73%", "78%", "82%"}, 1),
                new Question("<b>Is this program a combined degree with Computer Science?</b>",
                    new string[] {"Yes", "No", "Only for international students"}, 0)
            }
        ));
        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration and Economics",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements (Canadian Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Advanced Functions (MHF4U)</i>, <i>Calculus & Vectors (MCV4U)</i>, or <i>Math of Data Management (MDM4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% average of math courses with one Grade 12 U math at 70%</i>)\n" +
                "• <b>Mean Average:</b> <color=blue>84.7%</color>",

                "<b><size=38>International Students:</size></b>\n\n" +
                "• <color=red>Not available for international students</color>"
            },
            new Question[] {
                new Question("<b>What degree is awarded for this program?</b>",
                    new string[] {"B.Sc.", "B.Comm.", "B.A."}, 1),
                new Question("<b>What is the minimum math course average requirement?</b>",
                    new string[] {"70%", "73%", "75%"}, 0),
                new Question("<b>Is this program available to international students?</b>",
                    new string[] {"Yes", "No", "Only with special permission"}, 1)
            }
        ));

        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration and Mathematics",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements (Canadian Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> <i>Advanced Functions (MHF4U)</i> and <i>Calculus & Vectors (MCV4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in Grade 12 U math courses</i>)\n",

                "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> <i>Grade 12 Advanced Functions</i> and <i>Grade 12 Calculus & Vectors</i>. <b>English required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in Grade 12 math courses</i>)"
            },
            new Question[] {
                new Question("<b>Which math courses are required for this program?</b>",
                    new string[] {"Advanced Functions only", "Calculus & Vectors only", "Both Advanced Functions and Calculus"}, 2),
                new Question("<b>What is the overall minimum average requirement?</b>",
                    new string[] {"70%", "73%", "75%"}, 1),
                new Question("<b>Is Calculus & Vectors required for international students?</b>",
                    new string[] {"Yes", "No", "Only recommended"}, 0)
            }
        ));

        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration and Political Science",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements (Canadian Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Advanced Functions (MHF4U)</i>, <i>Calculus & Vectors (MCV4U)</i>, or <i>Math of Data Management (MDM4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in one Grade 12 U math course</i>)",

                "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Grade 12 Advanced Functions</i>, <i>Grade 12 Calculus & Vectors</i>, or <i>Grade 12 Math of Data Management</i>. <b>English required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in one Grade 12 math course</i>)"
            },
            new Question[] {
                new Question("<b>Which of these is NOT an accepted math course?</b>",
                    new string[] {"Data Management", "Calculus", "Geometry"}, 2),
                new Question("<b>What is the minimum math course requirement?</b>",
                    new string[] {"70%", "73%", "75%"}, 0),
                new Question("<b>Is this program available to international students?</b>",
                    new string[] {"Yes", "No", "Only through special arrangement"}, 0)
            }
        ));

        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration and Psychology",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements:</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Advanced Functions (MHF4U)</i>, <i>Calculus & Vectors (MCV4U)</i>, or <i>Math of Data Management (MDM4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in one Grade 12 U math course</i>)"
            },
            new Question[] {
                new Question("<b>Which degree is awarded in this program?</b>",
                    new string[] {"B.Sc.", "B.Comm.", "B.A."}, 1),
                new Question("<b>What is the overall minimum average requirement?</b>",
                    new string[] {"70%", "73%", "75%"}, 1),
                new Question("<b>Is Psychology a required admission course?</b>",
                    new string[] {"Yes", "No", "Only for international students"}, 1)
            }
        ));

        undergraduatePrograms.Add(new ProgramInfo(
            "Business Administration and Women’s & Gender Studies",
            new string[] {
                "<b><size=38>Program Overview:</size></b>\n" +
                "• <b>Honours 4-Year Program</b>\n" +
                "• Thesis Option Available\n" +
                "• <b>B.Comm.</b>",

                "<b><size=38>Admission Requirements:</size></b>\n\n" +
                "• <b>Course Requirements:</b> One of <i>Advanced Functions (MHF4U)</i>, <i>Calculus & Vectors (MCV4U)</i>, or <i>Math of Data Management (MDM4U)</i>. <b>English (ENG4U) required.</b>\n" +
                "• <b>Minimum Average:</b> <color=green>73%</color> (<i>70% in one Grade 12 U math course</i>)"
            },
            new Question[] {
                new Question("<b>What makes this program unique?</b>",
                    new string[] {"Focus on Gender Studies", "Double degree", "Mandatory co-op"}, 0),
                new Question("<b>What is the math course requirement?</b>",
                    new string[] {"Any one math course", "Two math courses", "No math required"}, 0),
                new Question("<b>Is there a thesis option?</b>",
                    new string[] {"Yes", "No", "Only for co-op students"}, 0)
            }
        ));

    undergraduatePrograms.Add(new ProgramInfo(
        "Industrial Engineering with Minor in Business Administration",
        new string[] {
            "<b><size=38>Program Overview:</size></b>\n" +
            "• <b>Co-op Available</b>\n" +
            "• <b>Honours 4-Year Program</b>\n" +
            "• Professional Engineering (PEng) eligible\n" +
            "• <b>BASC Degree</b>",

            "<b><size=38>Admission Requirements (Canadian Students):</size></b>\n\n" +
            "• <b>Course Requirements:</b> <i>Advanced Functions (MHF4U)</i>, <i>Chemistry (SCH4U)</i>, <i>Physics (SPH4U)</i>, <b>English (ENG4U)</b>\n" +
            "• <b>Recommended:</b> Calculus & Vectors (MCV4U)\n" +
            "• <b>Minimum Average:</b> <color=green>74%</color> (<i>74% in math/science courses</i>)\n" +
            "• <b>Mean Average:</b> <color=blue>87%</color>",

            "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
            "• <b>Course Requirements:</b> <i>Grade 12 Advanced Functions</i>, <i>Grade 12 Chemistry</i>, <i>Grade 12 Physics</i>, <b>English</b>\n" +
            "• <b>Recommended:</b> Grade 12 Calculus & Vectors\n" +
            "• <b>Minimum Average:</b> <color=green>74%</color> (<i>74% in math/science courses</i>)\n" +
            "• <b>Mean Average:</b> <color=blue>87%</color>"
        },
        new Question[] {
            new Question("<b>Which science courses are required?</b>",
                new string[] {"Chemistry & Physics", "Biology & Chemistry", "Physics only"}, 0),
            new Question("<b>What is the degree designation?</b>",
                new string[] {"B.Comm.", "BASC", "B.Eng."}, 1),
            new Question("<b>What professional designation is possible?</b>",
                new string[] {"CPA", "PEng", "CFA"}, 1)
        }
    ));

    // Add graduate programs (you can add more as needed)
    graduatePrograms.Add(new ProgramInfo(
        "Business Administration – Professional Accounting Specialization, MBA",
        new string[] {
            "<b><size=38>Program Overview:</size></b>\n" +
            "• <b>3 Semesters (12 Months)</b>\n" +
            "• Semester 1 (Face to Face), Semester 2 and 3 (Online)\n" +
            "• Fall and Summer Intake",

            "<b><size=38>Admission Requirements (Canadian Students):</size></b>\n\n" +
            "• <b>Degree:</b> Successful completion of a 120-credit hour undergraduate degree\n" +
            "• <b>GPA:</b> <color=green>70%</color> cumulative GPA in previous academic degree/last 20 courses/4 full-time semesters\n" +
            "• <b>Core Courses:</b> Minimum average of 70% CGPA in Core Preparatory Courses\n" +
            "• <b>GMAT Executive Assessment</b>\n" +
            "• <b>English Proficiency:</b> TOEFL (600 paper, 250 computer, 100 IBT), IELTS (7.0)\n" +
            "• No prior work experience required\n" +
            "• Updated resume, letter of intent, and two letters of reference\n" +
            "• Eligibility for CPA Professional Education Program (CPA PEP)",

            "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
            "• Same as Canadian students\n" +
            "• English proficiency required unless degree obtained from a North American institution"
        },
        new Question[] {
            new Question("<b>How many semesters does this program have?</b>",
                new string[] {"2", "3", "4"}, 1),
            new Question("<b>What is the minimum GPA requirement for admission?</b>",
                new string[] {"65%", "70%", "75%"}, 1),
            new Question("<b>Is prior work experience required?</b>",
                new string[] {"Yes", "No", "Only for international students"}, 1)
        }
    ));

    graduatePrograms.Add(new ProgramInfo(
        "Master of Business Administration – Full Time Program, MBA",
        new string[] {
            "<b><size=38>Program Overview:</size></b>\n" +
            "• <b>4 Semesters (16 Months)</b>\n" +
            "• Delivered In Person\n" +
            "• Intake: Fall\n" +
            "• No Work Experience Required",

            "<b><size=38>Admission Requirements (Canadian Students):</size></b>\n\n" +
            "• Completion of a 4-year bachelor degree from an accredited institution\n" +
            "• Cumulative GPA of <color=green>70%</color>, calculated based on the last 2 years of full-time studies\n" +
            "• One academic or professional reference\n" +
            "• GMAT Assessment (waivers available for CPA, CFA, P.Eng, LSAT, MCAT, or GPA ≥ 80%)\n" +
            "• Qualifying interview with Program Administrator/Director",

            "<b><size=38>Admission Requirements (International Students):</size></b>\n\n" +
            "• Same as Canadian students\n" +
            "• English proficiency required unless degree obtained from a North American institution or exempt countries"
        },
        new Question[] {
            new Question("<b>How long is the full-time MBA program?</b>",
                new string[] {"12 months", "16 months", "18 months"}, 1),
            new Question("<b>What is the minimum GPA requirement?</b>",
                new string[] {"65%", "70%", "75%"}, 1),
            new Question("<b>Can GMAT waivers be granted?</b>",
                new string[] {"Yes", "No", "Only for international students"}, 0)
        }
    ));

    graduatePrograms.Add(new ProgramInfo(
        "Master of Management, MM",
        new string[] {
            "<b><size=38>Program Overview:</size></b>\n" +
            "• <b>Four Semesters (16 Months)</b>\n" +
            "• Accelerated Option Available (12 Months)",

            "<b><size=38>Admission Requirements:</size></b>\n\n" +
            "<i>For Previous 4-Year Undergraduate Degree Holders:</i>\n" +
            "- Four-year bachelor-level degree in a related discipline approved by University of Windsor\n" +
            "- Equivalent of <color=green>70%</color> average or higher in undergraduate studies\n" +
            "- Letter of Intent outlining goals and qualifications\n" +
            "- Updated resume",

            "<i>For Previous 3-Year Undergraduate Degree Holders:</i>\n" +
            "- Four-year bachelor-level degree in a related discipline approved by University of Windsor\n" +
            "- Equivalent of <color=green>70%</color> average or higher in undergraduate studies\n" +
            "- Letter of Intent outlining goals and qualifications\n" +
            "- Updated resume"
        },
        new Question[] {
            new Question("<b>How many semesters does the MM program have?</b>",
                new string[] {"3", "4", "5"}, 1),
            new Question("<b>What is the accelerated option duration?</b>",
                new string[] {"10 months", "12 months", "14 months"}, 1),
            new Question("<b>What is the minimum GPA requirement?</b>",
                new string[] {"65%", "70%", "75%"}, 1)
        }
    ));
    }

    void SetupWelcomePanel()
    {
        welcomeText.text = "<b><size=45>Welcome to the Odette School of Business!</size></b>\n\n" +
                   "<b><size=40>Features:</size></b>\n" +
                   "• <b>Student Support</b>\n" +
                   "• <b>Co-Op & Career Development</b>\n" + 
                   "• <b>7 Areas of Specialization</b>\n" +
                   "• <b>1:17 Faculty-To-Student Ratio</b>\n" +
                   "• <b>Peer Mentoring</b>";

        undergraduateProgramsButton.onClick.AddListener(() => ShowProgramsPanel(undergraduateProgramsPanel));
        graduateProgramsButton.onClick.AddListener(() => ShowProgramsPanel(graduateProgramsPanel));
        nextButton.onClick.AddListener(NextSlide);
        startQuizButton.onClick.AddListener(StartQuiz);
        closeWelcomeButton.onClick.AddListener(ClosePanel);
    }

    void SetupProgramPanels()
    {
        SetupProgramButtons(undergraduatePrograms, undergraduateProgramsContent);
        SetupProgramButtons(graduatePrograms, graduateProgramsContent);
    }

    void SetupProgramButtons(List<ProgramInfo> programs, Transform content)
    {
        foreach (var program in programs)
        {
            Button button = Instantiate(programButtonPrefab, content);
            button.GetComponentInChildren<Text>().text = program.title;
            button.onClick.AddListener(() => ShowProgramPanel(program));
        }
        closeDetailsButton.onClick.AddListener(OpenQuiz);
    }

    void SetupQuizPanel()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void SetupResultsPanel()
    {
        restartButton.onClick.AddListener(RestartQuiz);
        closeQuizButton.onClick.AddListener(OpenQuiz);
    }

    private void HideAllPanels()
    {
        welcomePanel.SetActive(false);
        undergraduateProgramsPanel.SetActive(false);
        graduateProgramsPanel.SetActive(false);
        programPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultsPanel.SetActive(false);
    }

    void ShowWelcomePanel()
    {
        welcomePanel.SetActive(true);
        undergraduateProgramsPanel.SetActive(false);
        graduateProgramsPanel.SetActive(false);
        programPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultsPanel.SetActive(false);
    }

    void ShowProgramsPanel(GameObject panel)
    {
        welcomePanel.SetActive(false);
        undergraduateProgramsPanel.SetActive(false);
        graduateProgramsPanel.SetActive(false);
        panel.SetActive(true);
    }

    void ShowProgramPanel(ProgramInfo program)
    {
        currentProgram = program;
        currentSlideIndex = 0;
        programPanel.SetActive(true);
        undergraduateProgramsPanel.SetActive(false);
        graduateProgramsPanel.SetActive(false);
        UpdateProgramSlide();
    }

    void UpdateProgramSlide()
    {
        programPanel.SetActive(true);
        programTitleText.text = currentProgram.title;
        programContentText.text = currentProgram.content[currentSlideIndex];
        nextButton.gameObject.SetActive(currentSlideIndex < currentProgram.content.Length - 1);
        startQuizButton.gameObject.SetActive(currentSlideIndex == currentProgram.content.Length - 1);
    }

    public void NextSlide()
    {
        if (currentSlideIndex < currentProgram.content.Length - 1)
        {
            currentSlideIndex++;
            UpdateProgramSlide();
        }
    }

    public void StartQuiz()
    {
        programPanel.SetActive(false);
        quizPanel.SetActive(true);
        currentQuestionIndex = 0;
        score = 0;
        ShowQuestion();
    }

    void ShowQuestion()
    {
        Question question = currentProgram.questions[currentQuestionIndex];
        questionText.text = question.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = question.options[i];
            answerButtons[i].interactable = true;
        }
        feedbackText.gameObject.SetActive(false);
    }

    void CheckAnswer(int selectedIndex)
    {
        Question question = currentProgram.questions[currentQuestionIndex];
        bool isCorrect = selectedIndex == question.correctAnswerIndex;
        feedbackText.gameObject.SetActive(true);
        feedbackText.text = isCorrect ? "Correct!" : "Incorrect.";
        feedbackText.color = isCorrect ? Color.green : Color.red;

        if (isCorrect) score++;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].interactable = false;
        }

        StartCoroutine(NextQuestionDelay());
    }

    IEnumerator NextQuestionDelay()
    {
        yield return new WaitForSeconds(1.5f);
        currentQuestionIndex++;
        if (currentQuestionIndex < currentProgram.questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            ShowResults();
        }
    }

    void ShowResults()
    {
        quizPanel.SetActive(false);
        resultsPanel.SetActive(true);
        resultText.text = $"Your Score: {score}/{currentProgram.questions.Length}\n" +
                          $"You earned {score} knowledge coins!\n\n" +
                          GetFeedbackMessage();
        // Update the knowledge coin count in CoinCollider
        if (coinCollider != null)
        {
            coinCollider.AddCoins(score);
        }
    }

    string GetFeedbackMessage()
    {
        float percentage = (float)score / currentProgram.questions.Length;
        if (percentage == 1)
            return "Excellent! You have a great understanding of this program.";
        else if (percentage >= 0.7)
            return "Good job! You have a solid grasp of the program details.";
        else
            return "You might want to review the program information again.";
    }

    void RestartQuiz()
    {
        resultsPanel.SetActive(false);
        ShowProgramPanel(currentProgram);
    }

    void ClosePanel()
    {
        HideAllPanels();
        // Re-enable player movement and animation
        playerController.enabled = true;
        // Lock and hide the cursor again when quiz is closed
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}

public class ProgramInfo
{
    public string title;
    public string[] content;
    public Question[] questions;

    public ProgramInfo(string title, string[] content, Question[] questions)
    {
        this.title = title;
        this.content = content;
        this.questions = questions;
    }
}

public class Question
{
    public string questionText;
    public string[] options;
    public int correctAnswerIndex;

    public Question(string questionText, string[] options, int correctAnswerIndex)
    {
        this.questionText = questionText;
        this.options = options;
        this.correctAnswerIndex = correctAnswerIndex;
    }
}