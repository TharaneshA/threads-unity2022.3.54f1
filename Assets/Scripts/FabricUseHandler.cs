using UnityEngine;
using UnityEngine.UI;

public class FabricUseHandler : MonoBehaviour
{
    public GameObject fabricPrefab;  // Prefab for plain fabric
    public GameObject chainButton;   // Chain button reference
    public GameObject dropZone;      // DropZone reference
    public GameObject overlayPrefab; // T-shirt overlay prefab
    public GameObject tshirtPrefab;  // FINAL T-shirt prefab (with stitch logic)

    private GameObject currentFabricInstance;
    private GameObject overlayInstance;
    private RectTransform dropZoneRect;

    private void Start()
    {
        if (dropZone != null)
            dropZoneRect = dropZone.GetComponent<RectTransform>();
        else
            Debug.LogError("❌ DropZone not assigned in the Inspector!");
    }

    public void OnUseButtonPressed(Sprite fabricSprite)
    {
        if (currentFabricInstance != null)
            Destroy(currentFabricInstance);

        if (fabricPrefab == null || dropZone == null)
        {
            Debug.LogError("❌ Fabric prefab or DropZone not assigned!");
            return;
        }

        currentFabricInstance = Instantiate(fabricPrefab, dropZone.transform);
        Image fabricImage = currentFabricInstance.GetComponent<Image>();

        if (fabricImage == null)
        {
            Debug.LogError("❌ No Image component found on fabricPrefab!");
            return;
        }

        fabricImage.sprite = fabricSprite;
        fabricImage.raycastTarget = true;

        RectTransform fabricRT = currentFabricInstance.GetComponent<RectTransform>();
        if (fabricRT != null && dropZoneRect != null)
        {
            fabricRT.anchoredPosition = Vector2.zero;
            fabricRT.SetAsLastSibling();
        }

        TriggerChainDisable();
    }

    private void TriggerChainDisable()
    {
        Transform chainTransform = transform.root.Find("chain");
        if (chainTransform != null)
        {
            Transform chainButtonTransform = chainTransform.Find("Button");
            if (chainButtonTransform != null)
            {
                Button btn = chainButtonTransform.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke();
                    Debug.Log("🔒 Chain disabled with auto-animate.");
                }
                else Debug.LogError("❌ No Button component on Chain/Button!");
            }
            else Debug.LogError("❌ Button not found under Chain!");
        }
        else Debug.LogError("❌ Chain not found! Check root object.");
    }

    public void OnScissorsClick()
    {
        if (currentFabricInstance == null || overlayPrefab == null) return;

        if (overlayInstance == null)
        {
            overlayInstance = Instantiate(overlayPrefab, currentFabricInstance.transform);
            RectTransform overlayRT = overlayInstance.GetComponent<RectTransform>();

            if (overlayRT != null)
            {
                overlayRT.anchoredPosition = Vector2.zero;
                overlayRT.SetAsLastSibling();
                Debug.Log("✨ Overlay added for tracing.");
            }
            else
            {
                Debug.LogError("❌ No RectTransform found on overlayPrefab!");
            }
        }

        LockFabricDuringTracing();
    }

    public void LockFabricDuringTracing()
    {
        if (currentFabricInstance == null) return;

        DraggableFabric draggable = currentFabricInstance.GetComponent<DraggableFabric>();
        if (draggable != null)
        {
            draggable.SetDragEnabled(false);
            Debug.Log("🔒 Fabric locked for tracing.");
        }
    }

    public void EnableDragAfterTracing(GameObject target)
    {
        DraggableFabric draggable = target.GetComponent<DraggableFabric>();
        if (draggable != null)
        {
            draggable.SetDragEnabled(true);
            Debug.Log("✅ Dragging re-enabled.");
        }
    }

    public void OnTracingComplete()
    {
        if (currentFabricInstance == null || tshirtPrefab == null) return;

        Destroy(currentFabricInstance);

        GameObject tshirt = Instantiate(tshirtPrefab, dropZone.transform);
        RectTransform tshirtRT = tshirt.GetComponent<RectTransform>();

        if (tshirtRT != null && dropZoneRect != null)
        {
            tshirtRT.anchoredPosition = Vector2.zero;
            tshirtRT.SetAsLastSibling();
        }

        Debug.Log("👕 Final T-shirt prefab spawned.");
    }

    public void ReturnToDropZone()
    {
        if (currentFabricInstance == null || dropZoneRect == null) return;

        RectTransform fabricRT = currentFabricInstance.GetComponent<RectTransform>();
        if (fabricRT != null)
        {
            fabricRT.anchoredPosition = Vector2.zero;
            Debug.Log("📍 Fabric snapped back to DropZone.");
        }
    }
}
