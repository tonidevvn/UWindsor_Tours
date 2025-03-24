using UnityEngine;

public class QuizIconTrigger : MonoBehaviour
{
    public QuizManager quizManager;  // Reference to the QuizManager script
    public GameObject player;        // Reference to the player (can be the player GameObject or Camera)

    private void OnMouseDown()  // Detects when the icon is clicked
    {
        // Check if the player clicks on the icon and the quizManager is assigned
        if (quizManager != null)
        {
            quizManager.OpenQuiz();
            // Unlock the cursor so the user can interact with UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnMouseOver()  // Optional: Change color when the player is hovering over the icon
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.green;  // Change color to green when hovering
        }
    }

    private void OnMouseExit()  // Reset color when the player is not hovering over the icon
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.white;  // Reset color
        }
    }
}

