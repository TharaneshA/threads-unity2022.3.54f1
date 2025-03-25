using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public static SceneFader instance;
    public Image fadeImage;
    public float fadeDuration = 1f;
    private bool isFading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (fadeImage != null)
        {
            StartCoroutine(FadeIn()); // Always fade-in when a new scene is loaded
        }
    }

    public void FadeToScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut(sceneName));
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        isFading = true;
        float t = fadeDuration;
        while (t > 0)
        {
            t -= Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0); // Fully transparent
        isFading = false;
    }

    private System.Collections.IEnumerator FadeOut(string sceneName)
    {
        isFading = true;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, t / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1); // Fully black before scene load

        SceneManager.LoadScene(sceneName);

        // Ensure fade-in starts after scene is loaded
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeIn()); // Trigger fade-in after loading
    }
}
