using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFabric : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup missing on fabricPrefab! Adding one.");
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
{
    Debug.Log("Dragging started - Script Activated!"); // Check if function runs

    if (canvasGroup == null)
    {
        Debug.LogError("CanvasGroup is missing on fabric!");
        return;
    }

    canvasGroup.alpha = 0.8f;  
    canvasGroup.blocksRaycasts = false;
    Debug.Log("Blocks Raycasts Disabled: " + canvasGroup.blocksRaycasts);
}
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging fabric...");
        if (rectTransform == null)
        {
            Debug.LogError("RectTransform is NULL!");
            return;
        }
        if (canvas == null)
        {
            Debug.LogError("Canvas is NULL! Is this inside a UI Canvas?");
            return;
        }
        
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
{
    Debug.Log("Dragging ended");
    canvasGroup.alpha = 1f;
    canvasGroup.blocksRaycasts = true;  // Should re-enable raycasts
    Debug.Log("Blocks Raycasts Enabled: " + canvasGroup.blocksRaycasts);
}

}
