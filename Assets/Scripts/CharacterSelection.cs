using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
	public GameObject[] characters;

	
    [Header("Name Input")]
	public TMP_InputField nameInputField;
    public Color normalColor = new Color(255f, 255f, 255f, 85f);
    public Color selectedColor = new Color(255f, 255f, 255f, 125f);

	public int selectedCharacter = 0;
    AudioManager audioManager;

	private void Awake()
	{
		audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
		  // Load previously entered player name
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            nameInputField.text = PlayerPrefs.GetString("PlayerName");
        }
	}

	public void NextCharacter()
	{
		audioManager.PlayMouseClick();
		characters[selectedCharacter].SetActive(false);
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		characters[selectedCharacter].SetActive(true);
	}

	public void PreviousCharacter()
	{
		audioManager.PlayMouseClick();
		characters[selectedCharacter].SetActive(false);
		selectedCharacter--;
		if (selectedCharacter < 0)
		{
			selectedCharacter += characters.Length;
		}
		characters[selectedCharacter].SetActive(true);
	}

	public void StartGame()
	{
        audioManager.PlayMouseClick();
		PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        PlayerPrefs.SetString("PlayerName", nameInputField?.text ?? "Player");
		PlayerPrefs.Save();
		Loader.LoadGame();
	}

	
    public void InputNameSelect(String name)
    {
        nameInputField.image.color = selectedColor;
    }

    public void InputNameDeselect(String name)
    {
        nameInputField.image.color = normalColor;
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();
        Debug.Log("Player name saved: " + name);
    }
}
