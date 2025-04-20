using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadScene()
    {
        SceneFader.instance.FadeToScene(sceneToLoad);
    }
}
