using UnityEngine;

public class ClothInteraction : MonoBehaviour
{
    public GameObject cloth1; // Assign the dialog panel
    public GameObject cloth2; // Assign the dialog panel

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag("Cloth1"))
        {
            cloth1.SetActive(true);
            cloth2.SetActive(false);
        }
        else if (other.CompareTag("Player") && gameObject.CompareTag("Cloth2"))
        {
            cloth1.SetActive(false);
            cloth2.SetActive(true);
        }
    }
    
}
