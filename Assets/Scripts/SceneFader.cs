using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    public static SceneFader instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // ✅ Fade-in when scene loads
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName)); // ✅ Fade-out and switch scene
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0); // ✅ Fully transparent
    }

    private System.Collections.IEnumerator FadeOut(string sceneName)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1); // ✅ Fully black before load
        SceneManager.LoadScene(sceneName);
    }
}
