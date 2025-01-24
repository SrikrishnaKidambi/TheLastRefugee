using UnityEngine;

public class ReduceYPositionAfterDelay : MonoBehaviour
{
    public float t1 = 50f;  // Minimum random delay
    public float t2 = 120f;  // Maximum random delay
    public float reduceAmount = 4f;  // Amount by which Y position will reduce each second
    public float reductionDuration = 15f;  // Duration over which to reduce the Y position

    private float targetTime;  // Time after which the Y position starts reducing
    private bool reducingY = false;  // Flag to check if the Y position is being reduced

    void Start()
    {
        // Generate a random delay between t1 and t2
        float randomDelay = Random.Range(t1, t2);

        // Set the target time for when Y position starts reducing
        targetTime = Time.time + randomDelay;
    }

    void Update()
    {
        // Check if the random delay has passed and the Y reduction hasn't started yet
        if (!reducingY && Time.time >= targetTime)
        {
            reducingY = true;
        }

        // If the reduction has started, reduce the Y position slowly over time
        if (reducingY)
        {
            if (Time.time <= targetTime + reductionDuration)
            {
                // Reduce the Y position slowly by a small amount over time
                float reduction = reduceAmount * Time.deltaTime;
                transform.position -= new Vector3(0, reduction, 0);
            }
            else
            {
                // Stop reducing the Y position after the duration ends and deactivate the object
                reducingY = false;
                gameObject.SetActive(false);  // Deactivates the entire object
            }
        }
    }
}

