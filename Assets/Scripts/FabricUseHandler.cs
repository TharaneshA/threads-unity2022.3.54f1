using UnityEngine;
using UnityEngine.UI;

public class FabricUseHandler : MonoBehaviour
{
    public GameObject fabricPrefab;  // Prefab for fabric
    public GameObject chainButton;  // Chain button reference
    public GameObject dropZone;  // DropZone reference
    public GameObject overlayPrefab;  // T-shirt overlay prefab
    public Sprite tshirtSprite;  // T-shirt sprite after tracing
    private GameObject currentFabricInstance;
    private GameObject overlayInstance;
    private RectTransform dropZoneRect;
    private bool isTracingActive = false;

    private void Start()
    {
        if (dropZone != null)
        {
            dropZoneRect = dropZone.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("❌ DropZone not assigned in the Inspector!");
        }
    }

    // ✅ Display fabric when clicking UseButton
    public void OnUseButtonPressed(Sprite fabricSprite)
    {
        if (currentFabricInstance != null)
        {
            Destroy(currentFabricInstance);
        }

        if (fabricPrefab == null || dropZone == null)
        {
            Debug.LogError("❌ Fabric prefab or DropZone not assigned!");
            return;
        }

        // ✅ Instantiate fabric inside DropZone
        currentFabricInstance = Instantiate(fabricPrefab, dropZone.transform);
        Image fabricImage = currentFabricInstance.GetComponent<Image>();

        if (fabricImage == null)
        {
            Debug.LogError("❌ No Image component found on fabricPrefab!");
            return;
        }

        fabricImage.sprite = fabricSprite;
        fabricImage.raycastTarget = true;

        // ✅ Properly center and snap the fabric to DropZone
        RectTransform fabricRT = currentFabricInstance.GetComponent<RectTransform>();
        if (fabricRT != null)
        {
            fabricRT.anchoredPosition = Vector2.zero;
            fabricRT.SetAsLastSibling();  // Ensure fabric is above other UI elements
        }
        else
        {
            Debug.LogError("❌ No RectTransform found on fabricPrefab!");
        }

        // ✅ Trigger chain disable
        TriggerChainDisable();
    }

    // ✅ Auto Animate Disable for Chain Button
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
                    btn.onClick.Invoke();  // ✅ Auto Animate transition
                    Debug.Log("🔒 Chain disabled with auto-animate.");
                }
                else
                {
                    Debug.LogError("❌ No Button component found on Chain/Button!");
                }
            }
            else
            {
                Debug.LogError("❌ Button not found under Chain!");
            }
        }
        else
        {
            Debug.LogError("❌ Chain not found! Check root object.");
        }
    }

    // ✅ On First Click with Scissors, Show Overlay and Lock Fabric
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
                overlayRT.SetAsLastSibling();  // ✅ Bring overlay to front
                Debug.Log("✨ Overlay added for tracing.");
            }
            else
            {
                Debug.LogError("❌ No RectTransform found on overlayPrefab!");
            }
        }

        LockFabricDuringTracing();  // ✅ Lock fabric immediately
    }

    // ✅ Lock Fabric to Prevent Dragging
    public void LockFabricDuringTracing()
    {
        if (currentFabricInstance == null) return;

        DraggableFabric draggable = currentFabricInstance.GetComponent<DraggableFabric>();
        if (draggable != null)
        {
            draggable.SetDragEnabled(false);  // Disable drag immediately
            Debug.Log("🔒 Fabric locked for tracing.");
        }
    }

    // ✅ Unlock and Change to T-Shirt After Tracing
    public void OnTracingComplete()
    {
        if (currentFabricInstance == null || tshirtSprite == null) return;

        Image fabricImage = currentFabricInstance.GetComponent<Image>();
        if (fabricImage != null)
        {
            fabricImage.sprite = tshirtSprite;  // ✅ Transform to T-shirt sprite
            Debug.Log("👕 Fabric transformed into T-shirt!");
        }

        EnableDragAfterTracing();  // ✅ Re-enable drag after tracing
    }

    public void EnableDragAfterTracing()
    {
        if (currentFabricInstance == null) return;

        DraggableFabric draggable = currentFabricInstance.GetComponent<DraggableFabric>();
        if (draggable != null)
        {
            draggable.SetDragEnabled(true);  // ✅ Dragging re-enabled
            Debug.Log("✅ Dragging re-enabled after tracing.");
        }
    }

    public void ReturnToDropZone()
    {
        if (currentFabricInstance == null || dropZoneRect == null) return;

        RectTransform fabricRT = currentFabricInstance.GetComponent<RectTransform>();
        if (fabricRT != null)
        {
            fabricRT.anchoredPosition = Vector2.zero;  // ✅ Snap fabric to center of DropZone
            Debug.Log("📍 Fabric snapped back to DropZone.");
        }
    }
}
