using UnityEngine;
using UnityEngine.UI;

public class PropVanish : MonoBehaviour
{
    [SerializeField] private RainFallScript rainFallScript;
    [SerializeField] private Text collectMessage;
    [SerializeField] private float messageDisplayTime=2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            rainFallScript.hasMedicine = true;
            ShowMessage("You have collected the medicine successfully!");
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
