using UnityEngine;
using UnityEngine.UI;

public class FabricUseHandler : MonoBehaviour
{
    public GameObject fabricPrefab;   // Fabric prefab to instantiate
    public GameObject overlayPrefab;  // T-shirt outline prefab
    private GameObject currentFabricInstance;

    public Vector2 overlaySize = new Vector2(300f, 300f);  // Default overlay size

    public void OnUseButtonPressed(Sprite fabricSprite)
    {
        if (currentFabricInstance != null)
        {
            Destroy(currentFabricInstance);
        }

        if (fabricPrefab == null)
        {
            Debug.LogError("❌ Fabric prefab not assigned!");
            return;
        }

        // ✅ Instantiate fabric UI
        currentFabricInstance = Instantiate(fabricPrefab, transform);
        Image fabricImage = currentFabricInstance.GetComponent<Image>();

        if (fabricImage == null)
        {
            Debug.LogError("❌ No Image component found on fabricPrefab!");
            return;
        }

        fabricImage.sprite = fabricSprite;
        fabricImage.raycastTarget = true;

        RectTransform rt = currentFabricInstance.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("❌ No RectTransform found on fabricPrefab!");
            return;
        }

        rt.anchoredPosition = Vector2.zero;

        // ✅ Add TShirtOverlayHandler dynamically with overlay size
        AddOverlayHandler(currentFabricInstance);
    }

    private void AddOverlayHandler(GameObject fabricInstance)
    {
        if (fabricInstance.GetComponent<TShirtOverlayHandler>() == null)
        {
            TShirtOverlayHandler overlayHandler = fabricInstance.AddComponent<TShirtOverlayHandler>();
            overlayHandler.overlayPrefab = overlayPrefab;
            overlayHandler.overlaySize = overlaySize;  // Pass size to handler
        }
    }
}
