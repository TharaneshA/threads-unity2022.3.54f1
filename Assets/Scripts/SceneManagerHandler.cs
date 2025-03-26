using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHandler : MonoBehaviour
{
    public SceneFader sceneFader;
    // Go to Shop Scene
    public void LoadShopScene()
    {
        sceneFader.FadeToScene("ShopScene");
    }

    // Return to Workshop Scene
    public void LoadWorkshopScene()
    {
        SceneManager.LoadScene("Workshop");
    }
}
