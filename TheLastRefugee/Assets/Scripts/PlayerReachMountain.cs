using UnityEngine;
using UnityEngine.UI;

public class PlayerReachMountain : MonoBehaviour
{
    [SerializeField] private Text collectMessage;
    [SerializeField] private float messageDisplayTime = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger.");
            ShowMessage("You have completed the game successfully!");
        }
    }
    private void ShowMessage(string message)
    {
        collectMessage.text = message;
        collectMessage.enabled = true;
        Invoke(nameof(HideMessage), messageDisplayTime);
    }
    private void HideMessage()
    {
        collectMessage.enabled = false;
    }
}
