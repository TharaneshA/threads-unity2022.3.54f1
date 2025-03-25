using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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
            DontDestroyOnLoad(gameObject); // ✅ Keep FadeController persistent across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // ✅ Listen for scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn()); // ✅ Always fade in at the start
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
        fadeImage.color = new Color(0, 0, 0, 1); // ✅ Ensure fully black on scene load
        StartCoroutine(FadeIn());

        // ✅ Restart animations and refresh UI after scene transition
        Invoke("RestartAnimations", 0.2f);
        Invoke("ForceUIRefresh", 0.3f);
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
        fadeImage.color = new Color(0, 0, 0, 0); // ✅ Fully transparent
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
        fadeImage.color = new Color(0, 0, 0, 1); // ✅ Fully black before load
        SceneManager.LoadScene(sceneName);
    }

    private void RestartAnimations()
    {
        Debug.Log("Restarting animations after fade-in...");
        Animator[] animators = FindObjectsOfType<Animator>();
        Debug.Log("Animators Found: " + animators.Length);

        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.enabled = false; // ✅ Disable to reset animator state
                animator.enabled = true;  // ✅ Enable to rebind correctly
                animator.Rebind();        // ✅ Rebind to reset animation
                animator.Update(0);       // ✅ Force animator to refresh
            }
        }
    }

    private void ForceUIRefresh()
    {
        Canvas.ForceUpdateCanvases(); // ✅ Force UI to update after scene load
        EventSystem.current?.SetSelectedGameObject(null); // ✅ Reset EventSystem
    }
}
