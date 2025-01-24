using UnityEngine;
using UnityEngine.UI; // For UI elements

public class RainFallScript : MonoBehaviour
{
    [Header("Rain Settings")]
    public ParticleSystem rainParticleSystem;
    public AudioSource rainAudioSource;
    public float rainDelay = 5f;

    [Header("Thunder Settings")]
    public Light flashLight;
    public AudioClip thunderSound;
    public float thunderIntervalMin = 5f;
    public float thunderIntervalMax = 15f;
    public float flashDuration = 0.2f;
    public ParticleSystem lightningStrikeParticleSystem;
    public float lightningStrikeChance = 0.3f;

    [Header("Player Settings")]
    public GameObject player;
    public float playerHealth = 100f;
    private float rainStartTime = 0f;
    private bool isHealthDegrading = false;
    public bool isInsideHouse = false;

    [Header("UI Settings")]
    public Text healthText;

    [Header("Props Settings")]
    public bool hasMedicine = false;
    public bool hasRainCoat = false;
    public bool hasTorch = false;

    private float timeElapsed = 0f;
    private bool isRaining = false;

    private void Start()
    {
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
            flashLight.intensity = 0.5f;
        }
        if (lightningStrikeParticleSystem != null)
        {
            lightningStrikeParticleSystem.Stop();
        }

        // Update the health UI at the start
        UpdateHealthUI();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!isRaining && timeElapsed >= 20f)
        {
            isRaining = true;
            rainStartTime = Time.time;
            StartCoroutine(StartRainAfterDelay());
        }

        if (isRaining && !isHealthDegrading && Time.time - rainStartTime > 30f)
        {
            isHealthDegrading = true;
            StartCoroutine(DegradeHealthOverTime());
        }
        if (hasMedicine)
        {
            Debug.Log("Player has collected the medicine!!");
        }
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
            Debug.Log("Rain audio is playing.");
        }

        Debug.Log("Rain Started!");
        StartCoroutine(ThunderstormRoutine());
    }

    private System.Collections.IEnumerator ThunderstormRoutine()
    {
        while (true)
        {
            float interval = Random.Range(thunderIntervalMin, thunderIntervalMax);
            yield return new WaitForSeconds(interval);

            if (flashLight != null)
            {
                StartCoroutine(LightningFlash());
            }

            if (thunderSound != null)
            {
                AudioSource.PlayClipAtPoint(thunderSound, Camera.main.transform.position);
            }

            if (Random.value < lightningStrikeChance)
            {
                TriggerLightningStrike();
            }
        }
    }

    private System.Collections.IEnumerator DegradeHealthOverTime()
    {
        while (isRaining && playerHealth > 0)
        {
            if (!hasRainCoat)
            {
                playerHealth -= 0.5f;
                playerHealth = Mathf.Max(playerHealth, 0); // Clamp health to 0
            }

            UpdateHealthUI(); // Update the health display in the Canvas
            Debug.Log($"Player Health: {playerHealth}");

            if (playerHealth <= 0)
            {
                Debug.Log("Player has died due to exposure!");
                playerHealth = 0; // Ensure health is exactly 0
                UpdateHealthUI(); // Refresh UI after clamping to zero
                break;
            }

            yield return new WaitForSeconds(1f);
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
            flashLight.enabled = true;
            yield return new WaitForSeconds(flashDuration);
            flashLight.enabled = false;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {Mathf.Max(playerHealth, 0):0}"; // Ensure health doesn't go below 0
        }
    }
}