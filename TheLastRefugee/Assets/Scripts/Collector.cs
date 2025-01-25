using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public TMP_Text collectedMessage; // UI element to show the message
    private GameObject nearbyPad;       // Reference to the pad object the player is near
    public bool isCollected = false;    // Boolean to track if the pad is collected

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the range of a pad
        if (other.CompareTag("Collectible") && !isCollected)
        {
            nearbyPad = other.gameObject;
            isCollected = true;
            nearbyPad.SetActive(false);
            ShowDisplayMessage();
        }
    }

    private void ShowDisplayMessage()
    {
        
        collectedMessage.text = "You collected the exam answer script successfully";
        collectedMessage.gameObject.SetActive(true);
        // Hide the message after 2 seconds
        Invoke(nameof(HideCollectedMessage), 2f);
    }

    private void HideCollectedMessage()
    {
        collectedMessage.gameObject.SetActive(false);
    }
}
