using UnityEngine;
using UnityEngine.UI;

public class TorchCollect : MonoBehaviour
{
    [SerializeField] private RainFallScript rainFallScript;
    [SerializeField] private Text collectMessage;
    [SerializeField] private float messageDisplayTime = 2f;
    [SerializeField] private DrawOpen drawOpenScript;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is ready to collect the torch");
            gameObject.SetActive(false);
            rainFallScript.hasTorch = true;
            ShowMessage("You have collected the torch successfully!");
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
