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

        Debug.Log($"🧺 Inventory Updated: {fabricName} = {fabricInventory[fabricName]}");
    }

    public static int GetCount(string fabricName)
    {
        return fabricInventory.ContainsKey(fabricName) ? fabricInventory[fabricName] : 0;
    }
}
