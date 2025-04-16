using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    private static HashSet<string> ownedFabrics = new HashSet<string>();

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

    public static void AddFabric(string name)
    {
        ownedFabrics.Add(name);
    }

    public static bool HasFabric(string name)
    {
        return ownedFabrics.Contains(name);
    }

    public static HashSet<string> GetAllOwnedFabrics()
    {
        return ownedFabrics;
    }
}
