using UnityEngine;

public class TriggerSoundAfterDelay : MonoBehaviour
{
    public AudioClip soundEffect; // Assign your sound effect in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component to the GameObject if it doesn't exist
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the sound effect to the AudioSource
        if (soundEffect != null)
        {
            audioSource.clip = soundEffect;
        }

        // Trigger the sound effect after 35 seconds
        Invoke("PlaySoundEffect", 35f);
    }

    void PlaySoundEffect()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or soundEffect is missing!");
        }
    }
}
