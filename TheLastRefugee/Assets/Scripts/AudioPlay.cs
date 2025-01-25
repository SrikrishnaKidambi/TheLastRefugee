using UnityEngine;

public class SceneAudio : MonoBehaviour
{
    public AudioSource audioSource; // Assign this in the Inspector

    void Start()
    {
        // Play the audio when the scene starts
        audioSource.Play();
    }
}
