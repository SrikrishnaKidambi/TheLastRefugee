using UnityEngine;

public class RainFallScript : MonoBehaviour
{
    [Header("Rain Settings")]
    public ParticleSystem rainParticleSystem;
    public AudioSource rainAudioSource;
    public float rainDelay = 5f;

    [Header("Thunder Settings")]
    public Light flashLight; // A light to simulate lightning flashes
    public AudioClip thunderSound; // Thunder sound effect
    public float thunderIntervalMin = 5f; // Minimum time between thunder
    public float thunderIntervalMax = 15f; // Maximum time between thunder
    public float flashDuration = 0.2f; // Duration of lightning flash
    public ParticleSystem lightningStrikeParticleSystem; // The particle system for lightning strikes
    public float lightningStrikeChance = 0.3f; // 30% chance of a strike with thunder

    private void Start()
    {
        // Stop rain, thunder, and lightning effects at the start
        if (rainParticleSystem != null)
        {
            rainParticleSystem.Stop();
        }
        if (rainAudioSource != null)
        {
            rainAudioSource.Stop();
        }
        if (flashLight != null)
        {
            flashLight.enabled = false;
            flashLight.intensity = 0.5f; // Set the initial intensity of the flashlight
        }
        if (lightningStrikeParticleSystem != null)
        {
            lightningStrikeParticleSystem.Stop();
        }

        // Start the rain after a delay
        StartCoroutine(StartRainAfterDelay());
    }

    private System.Collections.IEnumerator StartRainAfterDelay()
    {
        yield return new WaitForSeconds(rainDelay);

        if (rainParticleSystem != null)
        {
            rainParticleSystem.Play();
        }

        if (rainAudioSource != null)
        {
            rainAudioSource.Play();
            Debug.Log("Rain audio is playing."); // Debug to confirm playback
        }

        Debug.Log("Rain Started!");
        StartCoroutine(ThunderstormRoutine());
    }


    private System.Collections.IEnumerator ThunderstormRoutine()
    {
        while (true)
        {
            // Wait for a random interval between thunder effects
            float interval = Random.Range(thunderIntervalMin, thunderIntervalMax);
            yield return new WaitForSeconds(interval);

            // Trigger lightning flash
            if (flashLight != null)
            {
                StartCoroutine(LightningFlash());
            }

            // Play thunder sound
            if (thunderSound != null)
            {
                AudioSource.PlayClipAtPoint(thunderSound, Camera.main.transform.position);
            }

            // Trigger the lightning strike particle effect occasionally
            if (Random.value < lightningStrikeChance)
            {
                TriggerLightningStrike();
            }
        }
    }

    private void TriggerLightningStrike()
    {
        if (lightningStrikeParticleSystem != null)
        {
            lightningStrikeParticleSystem.Play();
        }
        Debug.Log("Lightning Strike Occurred!");
    }

    private System.Collections.IEnumerator LightningFlash()
    {
        if (flashLight != null)
        {
            flashLight.enabled = true; // Turn on the flash light
            yield return new WaitForSeconds(flashDuration);
            flashLight.enabled = false; // Turn off the flash light
        }
    }
}
