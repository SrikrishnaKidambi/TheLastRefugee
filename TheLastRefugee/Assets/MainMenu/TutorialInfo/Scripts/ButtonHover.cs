using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f); // Scale when hovered
    [SerializeField] private Vector3 normalScale = new Vector3(1f, 1f, 1f);  // Normal scale
    [SerializeField] private float scaleSpeed = 0.2f; // Speed of scale transition

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>(); // Get the RectTransform of the button
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Scale the button up when hovered
        StopAllCoroutines();
        StartCoroutine(ScaleButton(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Scale the button back to normal when mouse exits
        StopAllCoroutines();
        StartCoroutine(ScaleButton(normalScale));
    }

    private System.Collections.IEnumerator ScaleButton(Vector3 targetScale)
    {
        Vector3 initialScale = rectTransform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < scaleSpeed)
        {
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / scaleSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rectTransform.localScale = targetScale;
    }
}
