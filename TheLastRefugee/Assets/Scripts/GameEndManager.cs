using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timer = 60f; // Set your timer duration here
    public GameObject player; // Assign the player GameObject in the Inspector
    public Collider stadiumCollider; // Assign the stadium's MeshCollider in the Inspector

    private bool timerExpired = false;

    void Update()
    {
        // Decrease the timer every frame
        timer -= Time.deltaTime;

        // Check if the timer has run out
        if (timer <= 0f && !timerExpired)
        {
            timerExpired = true;
            CheckPlayerLocation();
        }
    }

    void CheckPlayerLocation()
    {
        // Check if the player is inside the stadium's collider
        if (stadiumCollider.bounds.Contains(player.transform.position))
        {
            // Player is inside the stadium
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            // Player is not inside the stadium
            SceneManager.LoadScene("LoseScene");
        }
    }
}
