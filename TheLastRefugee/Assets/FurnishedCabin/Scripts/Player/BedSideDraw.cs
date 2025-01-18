using UnityEngine;

public class BedsideDrawerController : MonoBehaviour
{
    public float moveDistance = 0.5f; // Default to positive
    public float moveSpeed = 2.0f;

    private System.Collections.Generic.Dictionary<Transform, DrawerState> drawerStates = new System.Collections.Generic.Dictionary<Transform, DrawerState>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5.05f))
            {
                Debug.Log("Hit object: " + hit.collider.name);

                Transform hitTransform = hit.collider.transform;
                if (hitTransform.CompareTag("draw"))
                {
                    HandleDrawer(hitTransform);
                }
                else if (hitTransform.parent != null && hitTransform.parent.CompareTag("draw"))
                {
                    HandleDrawer(hitTransform.parent);
                }
                else
                {
                    Debug.Log("Hit object is not a drawer.");
                }
            }
        }
    }

    private void HandleDrawer(Transform drawerTransform)
        {
            if (!drawerStates.ContainsKey(drawerTransform))
            {
                drawerStates[drawerTransform] = new DrawerState(drawerTransform.localPosition, false);
            }

            DrawerState drawerState = drawerStates[drawerTransform];

            // Determine the correct target position based on current state
            Vector3 targetPosition = drawerState.IsOpen
                ? drawerState.InitialPosition // Move back to initial position when closing
                : drawerState.InitialPosition + new Vector3(0, 0, moveDistance); // Move forward when opening

            Debug.Log("Initial Position: " + drawerState.InitialPosition);
            Debug.Log("Target Position: " + targetPosition);

            StopAllCoroutines(); // Stop any ongoing movement
            StartCoroutine(MoveDrawer(drawerTransform, targetPosition));

            drawerState.IsOpen = !drawerState.IsOpen; // Toggle the state
        }


    private System.Collections.IEnumerator MoveDrawer(Transform drawerTransform, Vector3 targetLocalPosition)
    {
        float elapsedTime = 0;
        Vector3 startingLocalPosition = drawerTransform.localPosition;

        while (elapsedTime < moveSpeed)
        {
            drawerTransform.localPosition = Vector3.Lerp(startingLocalPosition, targetLocalPosition, (elapsedTime / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        drawerTransform.localPosition = targetLocalPosition;
    }

    private class DrawerState
    {
        public Vector3 InitialPosition { get; }
        public bool IsOpen { get; set; }

        public DrawerState(Vector3 initialPosition, bool isOpen)
        {
            InitialPosition = initialPosition;
            IsOpen = isOpen;
        }

        public Vector3 OpenPosition(float moveDistance)
        {
            return InitialPosition + new Vector3(0, 0, moveDistance);
        }
    }
}
