using System.Collections;
using TMPro;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    public GameObject dialogBox; // Assign the dialog panel
    public GameObject player; // Assign the player GameObject
    public MonoBehaviour playerMovementScript; // Assign the player's movement script
    public MonoBehaviour cameraMovementScript; // Assign the camera's movement script
    public float dialogDuration = 3f; // Duration to display the dialog

    private MeshRenderer[] childRenderers; // Store renderers of child objects
    private Collider coinCollider; // Collider of the coin GameObject

    private int coinCount = 0;

    public TMP_Text cointCountText;

    private void Start()
    {
        // Get the coin's collider
        coinCollider = GetComponent<Collider>();

        // Get the MeshRenderers of all child objects
        childRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PauseGame();
            ShowDialog();
            // StartCoroutine(CloseDialogAndResume());
            // StartCoroutine(RespawnCoin());
        }
    }
    private IEnumerator RespawnCoin()
    {
        yield return new WaitForSecondsRealtime(dialogDuration*2); // Wait for real time (unaffected by Time.timeScale)

        // Hide all child renderers
        foreach (MeshRenderer renderer in childRenderers)
        {
            renderer.enabled = true;
        }

        // Disable the collider
        coinCollider.enabled = true;
    }
    private void PauseGame()
    {
        // Disable player and camera movement scripts
        playerMovementScript.enabled = false;
        cameraMovementScript.enabled = false;

        Time.timeScale = 0f; // Pause game logic
    }

    public void ResumeGame()
    {
        dialogBox.SetActive(false); // Hide the dialog

        // Hide all child renderers
        foreach (MeshRenderer renderer in childRenderers)
        {
            renderer.enabled = false;
        }

        // Disable the collider
        coinCollider.enabled = false;

        // Enable player and camera movement scripts
        playerMovementScript.enabled = true;
        cameraMovementScript.enabled = true;

        Time.timeScale = 1f; // Resume game logic

        coinCount++;

        cointCountText.text = "Knowledge Coins: " + coinCount;

        StartCoroutine(RespawnCoin());
    }

    private void ShowDialog()
    {
        dialogBox.SetActive(true); // Show the pop-up
    }

    private IEnumerator CloseDialogAndResume()
    {
        yield return new WaitForSecondsRealtime(dialogDuration); // Wait for real time (unaffected by Time.timeScale)

        dialogBox.SetActive(false); // Hide the dialog

        // Hide all child renderers
        foreach (MeshRenderer renderer in childRenderers)
        {
            renderer.enabled = false;
        }

        // Disable the collider
        coinCollider.enabled = false;

        // ResumeGame(); // Resume player and camera movement
    }
}
