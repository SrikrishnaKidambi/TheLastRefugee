using UnityEngine;

public class HomeExitTrack : MonoBehaviour
{
    public RainFallScript rainFallScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rainFallScript.isInsideHouse = false;
            Debug.Log("Player is outside the house!!");
        }
    }
}

