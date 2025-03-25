using UnityEngine;
using TMPro;

public class CreditsScroll : MonoBehaviour
{
    public RectTransform creditsText;
    public float scrollSpeed = 50f;

    private void Update()
    {
        creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (creditsText.anchoredPosition.y >= creditsText.sizeDelta.y + 1000f)
        {
            creditsText.anchoredPosition = new Vector2(creditsText.anchoredPosition.x, -creditsText.sizeDelta.y);
        }
    }
}
