using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public GameObject sceneFaderPrefab;

    private void Awake()
    {
        if (SceneFader.instance == null)
        {
            Instantiate(sceneFaderPrefab);
        }
    }
}
