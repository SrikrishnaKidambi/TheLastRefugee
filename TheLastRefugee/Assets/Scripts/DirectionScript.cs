using System.Collections.Generic;
using UnityEngine;

public class ArrowGuidanceSystem : MonoBehaviour
{
    public Transform player;                // Player's transform
    public Transform medicalShop;           // Target (Medical Shop)
    public GameObject arrowPrefab;          // Arrow prefab
    public List<Transform> waypoints;       // List of waypoints
    public float arrowSpacing = 2f;         // Distance between arrows
    private List<GameObject> activeArrows;  // List to track active arrows

    void Start()
    {
        activeArrows = new List<GameObject>();
        GeneratePathArrows();
    }

    void Update()
    {
        UpdateArrows();
    }

    // Generate arrows along the waypoints
    void GeneratePathArrows()
    {
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Vector3 start = waypoints[i].position;
            Vector3 end = waypoints[i + 1].position;

            // Place arrows along the segment from start to end
            float distance = Vector3.Distance(start, end);
            int arrowCount = Mathf.FloorToInt(distance / arrowSpacing);

            for (int j = 0; j <= arrowCount; j++)
            {
                Vector3 position = Vector3.Lerp(start, end, j / (float)arrowCount);
                Quaternion rotation = Quaternion.LookRotation(end - start);
                GameObject arrow = Instantiate(arrowPrefab, position, rotation);
                activeArrows.Add(arrow);
            }
        }
    }

    // Update arrows based on player position
    void UpdateArrows()
    {
        for (int i = activeArrows.Count - 1; i >= 0; i--)
        {
            GameObject arrow = activeArrows[i];
            float distanceToPlayer = Vector3.Distance(player.position, arrow.transform.position);

            // Remove arrows that are behind the player
            if (distanceToPlayer < 1f)
            {
                Destroy(arrow);
                activeArrows.RemoveAt(i);
            }
        }

        // Add arrows dynamically as the player progresses (if necessary)
    }

    // (Optional) Draw debug lines for the waypoints
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
        }
    }
}
