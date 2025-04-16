using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHandler : MonoBehaviour
{
    public SceneFader sceneFader;
    // Go to Shop Scene
    public void LoadHallScene()
    {
        sceneFader.FadeToScene("HallScene");
    }

    // Return to Workshop Scene
    public void LoadWorkshopScene()
    {
        SceneManager.LoadScene("Workshop");
    }
}
