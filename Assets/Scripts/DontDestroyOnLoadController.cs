using UnityEngine;

public class DontDestroyOnLoadController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // ✅ Keep this object alive during scene transitions
    }
}
