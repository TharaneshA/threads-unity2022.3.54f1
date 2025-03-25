using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public static FadeController instance;
    public Image fadeImage;
    public float fadeDuration = 1f;
    private bool isFading = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep canvas persistent
            SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // Always start with a fade-in
    }

    public void FadeToScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeOut(sceneName));
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        fadeImage.color = new Color(0, 0, 0, 1); // Fully black on load
        StartCoroutine(FadeIn()); // Fade-in after load
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
        fadeImage.color = new Color(0, 0, 0, 1); // Fully black before load
        SceneManager.LoadScene(sceneName);
    }
}
