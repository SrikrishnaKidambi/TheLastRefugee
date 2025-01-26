using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class ChangeSceneOnContact : MonoBehaviour
{
    public float timer = 60f; // Set your timer duration here
    public GameObject player; // Assign the player GameObject in the Inspector
    public Collider stadiumCollider; // Assign the stadium's MeshCollider in the Inspector

    private bool timerExpired = false;

    void Update(){
         if (stadiumCollider.bounds.Contains(player.transform.position))
        {
             timer -= Time.deltaTime;

        // Check if the timer has run out
        if (timer <= 0f && !timerExpired)
        {
            timerExpired = true;
            // Player is inside the stadium
            SceneManager.LoadScene("WinScene");
        }

        }
    }
}

//FINAL COMMIT : 22:07 26-01-2025
