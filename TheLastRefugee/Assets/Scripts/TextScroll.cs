using UnityEngine;
using TMPro;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component
    [SerializeField] private float scrollSpeed = 50f;     // Speed of scrolling

    private RectTransform textRectTransform;              // RectTransform of the text
    private RectTransform maskRectTransform;              // RectTransform of the mask

    private void Start()
    {
        textRectTransform = textMeshPro.GetComponent<RectTransform>();
        maskRectTransform = GetComponent<RectTransform>();

        // Reset text position to start from the top
        Vector2 newPos = textRectTransform.anchoredPosition;
        newPos.y = 0;
        textRectTransform.anchoredPosition = newPos;
    }

    private void Update()
    {
        // Scroll vertically with input
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
            Vector2 newPos = textRectTransform.anchoredPosition;

            // Ensure scrolling stays within bounds
            newPos.y = Mathf.Clamp(newPos.y + scrollAmount, 0, Mathf.Max(0, textRectTransform.sizeDelta.y - maskRectTransform.sizeDelta.y));
            textRectTransform.anchoredPosition = newPos;
        }
    }
}
