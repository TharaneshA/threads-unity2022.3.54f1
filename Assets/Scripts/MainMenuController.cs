using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverScale = 1.2f;
    public float animationSpeed = 10f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    // 🔁 Replace SceneFader with direct scene load
    public void StartGame()
    {
        SceneManager.LoadScene("Workshop");  // Replace with your Workshop scene name
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");  // Replace with your Credits scene name
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(transform, originalScale * hoverScale, 0.1f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleButton(transform, originalScale, 0.1f));
    }

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

        button.localScale = targetScale;
    }
}
