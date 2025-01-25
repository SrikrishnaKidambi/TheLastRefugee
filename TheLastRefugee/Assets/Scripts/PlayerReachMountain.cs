using UnityEngine;
using UnityEngine.UI;

public class PlayerReachMountain : MonoBehaviour
{
    [SerializeField] private Text collectMessage;         // The message to display
    [SerializeField] private float messageDisplayTime = 2f; // Duration to display the message
    [SerializeField] private AudioClip successBGM;        // The audio clip to play

    private AudioSource audioSource;                      // Reference to the AudioSource component

    private void Start()
    {
        // Ensure the GameObject has an AudioSource and assign it
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger.");
            ShowMessage("You have completed the game successfully!");
            PlaySuccessAudio(); // Play the audio clip
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

    private void PlaySuccessAudio()
    {
        if (successBGM != null)
        {
            audioSource.clip = successBGM; // Assign the audio clip
            audioSource.Play();           // Play the audio
        }
        else
        {
            Debug.LogWarning("No audio clip assigned for successBGM.");
        }
    }
}
