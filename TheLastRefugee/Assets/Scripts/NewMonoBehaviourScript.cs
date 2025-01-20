using System.Collections;
using UnityEngine;

public class FloodController : MonoBehaviour
{
    public bool startFlood = false;  // Trigger to start the flood
    public float delayBeforeFlood = 10f;  // Delay time in seconds before the flood starts
    public float expansionSpeed = 1f;  // Speed at which the flood expands horizontally
    public float maxFloodScale = 100f;  // Maximum horizontal expansion of the flood
    public float floodFlowSpeed = 2f;  // Speed of flood flowing down the hill
    public float floodMass = 5f;  // Mass of the flood object for realistic physics

    public AudioClip floodSound;  // Sound to play during the flood
    public float soundFadeDuration = 3f;  // Time for the sound to fade in

    private Rigidbody rb;  // Rigidbody for physics simulation
    private AudioSource audioSource;  // Audio source for flood sounds
    private Vector3 initialScale;

    void Start()
    {
        // Record the initial scale of the water object
        initialScale = transform.localScale;

        // Add a Rigidbody for gravity-based movement
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;  // Enable gravity
        rb.mass = floodMass;  // Set the mass for realistic physics
        rb.isKinematic = true;  // Keep it stationary until the flood starts

        // Add an AudioSource for flood sounds
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = floodSound;
        audioSource.loop = true;
        audioSource.volume = 0f;  // Start muted
        audioSource.playOnAwake = false;

        // Start the flood after a delay
        StartCoroutine(TriggerFloodAfterDelay());
    }

    void Update()
    {
        if (startFlood)
        {
            // Allow the water to flow down the hill
            rb.isKinematic = false;

            // Expand the flood area horizontally (X and Z axes)
            if (transform.localScale.x < initialScale.x + maxFloodScale)
            {
                transform.localScale += new Vector3(expansionSpeed, 0, expansionSpeed) * Time.deltaTime;
            }

            // Apply a force to simulate the water rolling down the hill
            Vector3 flowDirection = new Vector3(0, -1, floodFlowSpeed);  // Downward slope direction
            rb.AddForce(flowDirection * rb.mass);
        }
    }

    // Coroutine to start the flood after a delay
    private IEnumerator TriggerFloodAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeFlood);

        // Enable the flood and start the sound effect
        startFlood = true;

        // Gradually fade in the flood sound
        audioSource.Play();
        StartCoroutine(FadeInSound());
    }

    // Coroutine to fade in the flood sound
    private IEnumerator FadeInSound()
    {
        float elapsedTime = 0f;
        while (elapsedTime < soundFadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / soundFadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f;  // Ensure full volume
    }
}
