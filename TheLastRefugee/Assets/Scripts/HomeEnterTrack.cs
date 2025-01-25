using UnityEngine;

public class HomeEnterTrack : MonoBehaviour
{
    public RainFallScript rainFallScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rainFallScript.isInsideHouse = true;
            Debug.Log("Player is inside the house!!");
        }
    }
}
