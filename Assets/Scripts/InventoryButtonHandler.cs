using UnityEngine;
using UnityEngine.UI;

public class InventoryButtonHandler : MonoBehaviour
{
    public Sprite fabricSprite;  // Assign the fabric sprite in Inspector
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

        Debug.Log("✅ Use button clicked! Sending sprite to FabricUseHandler.");
        useHandler.OnUseButtonPressed(fabricSprite);
    }
}
