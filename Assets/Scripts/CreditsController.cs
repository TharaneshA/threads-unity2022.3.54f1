using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        if (SceneFader.instance != null)
        {
            SceneFader.instance.FadeToScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
