using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] characterPrefabs;
	public Avatar[] characterAvatars;
	public Transform spawnPoint;
	public TMP_Text label;
	private Animator animator;

    void Awake()
    {
        if (animator != null && !animator.isInitialized)
        {
            animator.Rebind();
            animator.Update(0f);
        }
    }

    void Start()
	{
    	animator = GetComponent<Animator>();

		int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
		
		// Active the selected character
		for (int i = 0; i < characterPrefabs.Length; i++)
		{
			characterPrefabs[i].SetActive(i == selectedCharacter);
		}
		// label.text = characterPrefabs[selectedCharacter].name;
		if (characterAvatars.Length > selectedCharacter && characterAvatars[selectedCharacter] != null)
		{
			animator.avatar = characterAvatars[selectedCharacter];
			Debug.Log($"Assigned avatar {characterAvatars[selectedCharacter].name} to Animator on Player.");
		}
		else
		{
			Debug.LogWarning("Avatar missing or index out of range!");
		}
	}
}
