using UnityEngine;
using UnityEngine.UI;

public class HighAltitudeEntry : MonoBehaviour
{
    [SerializeField] private Text messageText;
    [SerializeField] private FloodScript floodScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            floodScript.reachedHighAltitude = true;
            messageText.text = "You are reaching high altitude. Go forward to complete the game with a surprise";
        }
    }
    private void OnTriggerExit(Collider other)
    {
        floodScript.reachedHighAltitude= false;
        messageText.text = "";
    }
}
