using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSceneChange : MonoBehaviour
{
    // This method can be attached to the button's OnClick event in the inspector.
    public void LoadRandomObjective()
    {
        // List of possible scene names
        string[] scenes = { "Objective1", "Objective2", "Objective3" };

        // Select a random scene from the list
        string randomScene = scenes[Random.Range(0, scenes.Length)];

        // Load the randomly selected scene
        SceneManager.LoadScene(randomScene);
    }
}
