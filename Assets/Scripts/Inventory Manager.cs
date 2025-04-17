using System.Collections.Generic;
using UnityEngine;

public static class InventoryManager
{
    private static Dictionary<string, int> fabricInventory = new Dictionary<string, int>();

    public static void AddFabric(string fabricName)
    {
        if (fabricInventory.ContainsKey(fabricName))
            fabricInventory[fabricName]++;
        else
            fabricInventory[fabricName] = 1;

        Debug.Log($"🧺 Added {fabricName} to inventory. Count: {fabricInventory[fabricName]}");
    }

    public static void UseFabric(string fabricName)
    {
        if (fabricInventory.ContainsKey(fabricName) && fabricInventory[fabricName] > 0)
        {
            fabricInventory[fabricName]--;
            Debug.Log($"🧵 Used {fabricName}. Remaining: {fabricInventory[fabricName]}");
        }
    }

    public static int GetCount(string fabricName)
    {
        return fabricInventory.ContainsKey(fabricName) ? fabricInventory[fabricName] : 0;
    }
    public static void DecrementFabric(string fabricName)
{
    if (fabricInventory.ContainsKey(fabricName) && fabricInventory[fabricName] > 0)
    {
        fabricInventory[fabricName]--;
        Debug.Log($"📉 Used 1 {fabricName}. Remaining: {fabricInventory[fabricName]}");
    }
}

}
