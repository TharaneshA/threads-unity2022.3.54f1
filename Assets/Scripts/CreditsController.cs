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
     
        SceneManager.LoadScene("MainMenu");

    }
}
