using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReachMountain : MonoBehaviour
{
    [SerializeField] private Text collectMessage;         // The message to display
    [SerializeField] private float messageDisplayTime = 2f; // Duration to display the message
    [SerializeField] private AudioClip successBGM;        // The audio clip to play

    private AudioSource audioSource;                      // Reference to the AudioSource component
    private bool hasPlayedSuccessAudio = false;           // Flag to check if the audio has been played

    [SerializeField] private UnityAndGeminiV3 unityGeminiScript;
    [SerializeField] private TMP_Text evalautionText;
    [SerializeField] private Image evalautionBackground;
    private void Start()
    {
        if (unityGeminiScript != null)
        {
            unityGeminiScript.enabled = false;
        }
        // Ensure the GameObject has an AudioSource and assign it
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayedSuccessAudio) // Check if audio has not been played
        {
            Debug.Log("Player has reached the mountain.");
            ShowMessage("You have completed the game successfully!");
            PlaySuccessAudio(); // Play the audio clip
            hasPlayedSuccessAudio = true; // Set the flag to true to prevent further playback
            Invoke(nameof(EnableUnityGeminiScript), messageDisplayTime);
            //Time.timeScale = 0f;
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
    private void EnableUnityGeminiScript()
    {
        if (evalautionBackground != null)
        {
            evalautionBackground.gameObject.SetActive(true);
        }
        if (evalautionText != null){
            evalautionText.gameObject.SetActive(true);
        }
        if (unityGeminiScript != null)
        {
            unityGeminiScript.enabled = true;
            Debug.Log("UnityGemini Script has been enabled");
        }
    }
}
