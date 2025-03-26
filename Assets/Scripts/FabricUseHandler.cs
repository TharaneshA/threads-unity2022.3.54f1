using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FabricUseHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public GameObject fabricPrefab;  // Prefab assigned in Unity Inspector
    private GameObject currentFabricInstance;
    public float dragSpeed = 5f;  // 🔹 Adjust this to control drag speed

    public void OnUseButtonPressed(Sprite fabricSprite)
    {
        Debug.Log("✅ Received Use button click event!");

        if (currentFabricInstance != null)
        {
            Debug.Log("🔄 Destroying previous fabric instance.");
            Destroy(currentFabricInstance);
        }

        if (fabricPrefab == null)
        {
            Debug.LogError("❌ Fabric prefab is not assigned in the Inspector!");
            return;
        }

        // Instantiate fabric UI object
        currentFabricInstance = Instantiate(fabricPrefab, transform);
        Debug.Log("✅ Fabric instantiated!");

        Image fabricImage = currentFabricInstance.GetComponent<Image>();
        if (fabricImage == null)
        {
            Debug.LogError("❌ No Image component found on FabricPrefab!");
            return;
        }

        fabricImage.sprite = fabricSprite;
        fabricImage.raycastTarget = true; // ✅ Enable Raycast Target automatically
        Debug.Log("✅ Fabric sprite set successfully & Raycast Target enabled.");

        RectTransform rt = currentFabricInstance.GetComponent<RectTransform>();
        if (rt == null)
        {
            Debug.LogError("❌ No RectTransform found on FabricPrefab!");
            return;
        }

        rt.anchoredPosition = Vector2.zero; // Center it on the screen
        Debug.Log("✅ Fabric positioned at the center.");
        Debug.Log("🔍 Fabric Color: " + fabricImage.color);
        
        if (currentFabricInstance.transform.parent == null)
            Debug.LogError("❌ Fabric instance is not inside a Canvas!");
        else
            Debug.Log("✅ Fabric is inside: " + currentFabricInstance.transform.parent.name);
        
        Debug.Log("🔍 Is Fabric Active? " + currentFabricInstance.activeSelf);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentFabricInstance == null)
        {
            Debug.LogWarning("⚠️ No fabric instance to drag!");
            return;
        }
        Debug.Log("🔄 Dragging started.");
        currentFabricInstance.GetComponent<Image>().raycastTarget = false; // ❌ Disable to avoid blocking interactions while dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentFabricInstance == null)
        {
            Debug.LogWarning("⚠️ No fabric instance to move!");
            return;
        }

        RectTransform rt = currentFabricInstance.GetComponent<RectTransform>();

        // 🔹 Smoothly move the fabric towards the cursor position
        Vector2 targetPosition = eventData.position;
        rt.position = Vector2.Lerp(rt.position, targetPosition, Time.deltaTime * dragSpeed);

        Debug.Log($"📍 Fabric smoothly moved to {rt.position}");
    }

    public void OnEndDrag(PointerEventData eventData)
{
    
    if (currentFabricInstance == null)
    {
        Debug.LogWarning("⚠️ No fabric instance to drop!");
        return;
    }

    Debug.Log("✅ Dragging ended.");

    // Check if dropped inside a drop zone
    GameObject dropZone = GameObject.Find("TailoringDropZone");
    if (dropZone != null)
    {
        RectTransform dropZoneRT = dropZone.GetComponent<RectTransform>();
        RectTransform fabricRT = currentFabricInstance.GetComponent<RectTransform>();

        if (RectTransformUtility.RectangleContainsScreenPoint(dropZoneRT, Input.mousePosition))
        {
            fabricRT.anchoredPosition = dropZoneRT.anchoredPosition;
            Debug.Log("🎯 Fabric placed in drop zone!");
        }
        else
        {
            // If dropped outside, return to center
            fabricRT.anchoredPosition = Vector2.zero;
            Debug.Log("🔄 Fabric returned to center.");
        }
    }
    GameObject hoveredObject = eventData.pointerCurrentRaycast.gameObject;
if (hoveredObject != null)
{
    Debug.Log("🟢 Dropped over: " + hoveredObject.name);
}
else
{
    Debug.Log("🔴 Dropped over nothing!");
}

    Debug.Log("✅ Dragging ended. Checking drop zone...");
}

}
