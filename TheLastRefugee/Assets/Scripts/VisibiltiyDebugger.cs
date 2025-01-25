using UnityEngine;

public class VisibilityDebugger : MonoBehaviour
{
    private bool lastState;

    void Start()
    {
        lastState = gameObject.activeSelf;
    }

    void Update()
    {
        if (gameObject.activeSelf != lastState)
        {
            Debug.Log($"{gameObject.name} visibility changed to: {gameObject.activeSelf}", this);
            lastState = gameObject.activeSelf;
        }
    }
}
