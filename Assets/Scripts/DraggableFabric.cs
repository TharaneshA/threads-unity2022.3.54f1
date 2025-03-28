using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFabric : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool isDragEnabled = true;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDragEnabled) return;
        Debug.Log("🔄 Drag started.");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragEnabled) return;
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragEnabled) return;

        GameObject dropZone = GameObject.Find("DropZone");
        if (dropZone != null)
        {
            RectTransform dropZoneRT = dropZone.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(dropZoneRT, Input.mousePosition))
            {
                rectTransform.anchoredPosition = Vector2.zero;  // ✅ Snap to DropZone
                Debug.Log("🎯 Fabric snapped to DropZone.");
            }
            else
            {
                rectTransform.anchoredPosition = originalPosition;  // Snap back to original
                Debug.Log("🔄 Fabric returned to original position.");
            }
        }
    }

    public void SetDragEnabled(bool enabled)
    {
        isDragEnabled = enabled;
        Debug.Log(enabled ? "✅ Dragging enabled." : "❌ Dragging disabled.");

        // Lock fabric in place if drag is disabled
        if (!enabled && rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero;  // Lock fabric in DropZone
        }
    }

}
