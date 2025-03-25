using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.2f; // Scale factor when hovering
    public float animationSpeed = 10f; // Speed of the scale animation
    private Vector3 originalScale;

    // Start and Credits button references
    public void StartGame()
    {
        SceneManager.LoadScene("Workshop"); // Load Workshop.unity
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits"); // Load Credits.unity
    }

    void Start()
    {
        // Store original scale when the script starts
        originalScale = transform.localScale;
    }

    // When mouse hovers over the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines(); // Stop any ongoing scaling coroutine
        StartCoroutine(ScaleButton(transform, originalScale * hoverScale, 0.1f));
    }

    // When mouse exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines(); // Stop any ongoing scaling coroutine
        StartCoroutine(ScaleButton(transform, originalScale, 0.1f));
    }

    // Coroutine to smoothly scale button
    private System.Collections.IEnumerator ScaleButton(Transform button, Vector3 targetScale, float duration)
    {
        Vector3 currentScale = button.localScale;
        float time = 0;

        while (time < duration)
        {
            button.localScale = Vector3.Lerp(currentScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        button.localScale = targetScale; // Set final scale
    }
}
