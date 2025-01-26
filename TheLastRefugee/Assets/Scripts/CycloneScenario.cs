using UnityEngine;
using TMPro;

public class CycloneScenario : MonoBehaviour
{
    public TMP_Text messageText;        // Reference to UI text for displaying messages
    public float proximityRange = 5f;  // Range to detect proximity to objects
    [SerializeField] private int proximityCounter = 0;  // Counter to track how many times the player gets close
    private float proximityCooldown = 4f;  // Time in seconds before the proximityCounter can increment again
    private float lastProximityTime = 0f; // Time when proximity was last detected

    void Update()
    {
        DetectNearbyObjects();
    }

    private void DetectNearbyObjects()
    {
        // Find all objects with the "Danger" tag (trees, windmills, poles)
        GameObject[] dangerObjects = GameObject.FindGameObjectsWithTag("Danger");

        foreach (GameObject obj in dangerObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            // Check if the player is within proximity
            if (distance <= proximityRange)
            {
                HandleProximity(obj);
                break; // Only count one object per update
            }
        }
    }

    private void HandleProximity(GameObject obj)
    {
        // Check if enough time has passed before incrementing the counter
        if (Time.time - lastProximityTime >= proximityCooldown)
        {
            proximityCounter++;
            lastProximityTime = Time.time; // Update the time when the proximity was last detected

            // Warn the player at count 3
            if (proximityCounter == 3 || proximityCounter == 4)
            {
                DisplayMessage("You are going close to the trees or poles or windmills. Please stay away!");
            }
            // Trigger fall animation and game-over at count 5
            else if (proximityCounter == 5)
            {
                DisplayMessage("Game Over! The tree has fallen due to the cyclone and player may die.");
                TriggerFallAnimation(obj);
            }
        }
    }

    private void TriggerFallAnimation(GameObject obj)
    {
        Animator animator = obj.GetComponent<Animator>();
        AudioSource audioSource = obj.GetComponent<AudioSource>();

        if (animator != null)
        {
            string parentName = obj.transform.parent.name;

            if (parentName == "Trees")
            {
                animator.Play("TreeAnimation");
            }
            else if (parentName == "WindMills")
            {
                animator.Play("WindmilAnimation");
            }
            else if (parentName == "StreetLights")
            {
                animator.Play("LightAnimation");
            }
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"No AudioSource attached to {obj.name}. Audio will not play.");
            }
        }
    }

    private void DisplayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        if (message.Contains("Game Over"))
        {
            // Pause the game after a short delay to allow the message to display
            Invoke(nameof(PauseGame), 0.1f);
        }
        else
        {
            // Hide the message after 5 seconds for non-Game Over messages
            Invoke(nameof(HideMessage), 5f);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
    }

    private void HideMessage()
    {
        messageText.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
        messageText.gameObject.SetActive(false); // Hide the message
    }
}
