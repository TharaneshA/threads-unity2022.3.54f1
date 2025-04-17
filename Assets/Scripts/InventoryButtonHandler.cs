using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonHandler : MonoBehaviour
{
    public Sprite fabricSprite;
    public string fabricName;  // Must match name used in InventoryManager
    private FabricUseHandler useHandler;

    private void Start()
    {
        useHandler = FindObjectOfType<FabricUseHandler>();
        if (useHandler == null)
            Debug.LogError("❌ FabricUseHandler not found in the scene!");
    }

    public void OnUseButtonClick()
    {
        if (fabricSprite == null)
        {
            Debug.LogError("❌ No fabric sprite assigned to this button!");
            return;
        }

        if (!CheckAvailability())
        {
            Debug.LogWarning($"❌ {fabricName} not available in inventory!");
            return;
        }

        useHandler.OnUseButtonPressed(fabricSprite);
        InventoryManager.DecrementFabric(fabricName);  // Decrease count on use
    }

    public bool CheckAvailability()
    {
        return InventoryManager.GetCount(fabricName) > 0;
    }
}
