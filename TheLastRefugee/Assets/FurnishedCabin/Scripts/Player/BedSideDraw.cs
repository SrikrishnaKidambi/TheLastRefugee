using UnityEngine;

public class BedsideDrawerController : MonoBehaviour
{
    public float moveDistance = 0.5f;
    public float moveSpeed = 2.0f;
    public Camera playerCamera;
    public KeyCode toggleKey = KeyCode.P; // Key to trigger the drawer

    private System.Collections.Generic.Dictionary<Transform, DrawerState> drawerStates = new System.Collections.Generic.Dictionary<Transform, DrawerState>();

    void Update()
    {
        // Listen for the keyboard key press
        if (Input.GetKeyDown(toggleKey))
        {
            TryToggleDrawer();
        }

        // Listen for mouse click
        if (Input.GetMouseButtonDown(0))
        {
            TryToggleDrawer();
        }
    }

    private void TryToggleDrawer()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5.05f))
        {
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

    private void HandleDrawer(Transform drawerTransform)
    {
        if (!drawerStates.ContainsKey(drawerTransform))
        {
            drawerStates[drawerTransform] = new DrawerState(drawerTransform.localPosition, false);
        }

        DrawerState drawerState = drawerStates[drawerTransform];
        Vector3 targetPosition = drawerState.IsOpen
            ? drawerState.InitialPosition
            : drawerState.InitialPosition + new Vector3(0, 0, moveDistance);

        StopAllCoroutines();
        StartCoroutine(MoveDrawer(drawerTransform, targetPosition));
        drawerState.IsOpen = !drawerState.IsOpen;
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
    }
}
