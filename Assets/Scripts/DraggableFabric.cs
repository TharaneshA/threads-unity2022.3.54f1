using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFabric : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private bool isSnapped = false;

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
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject dropZone = GameObject.Find("TailoringDropZone");
        if (dropZone != null)
        {
            RectTransform dropZoneRT = dropZone.GetComponent<RectTransform>();
            if (RectTransformUtility.RectangleContainsScreenPoint(dropZoneRT, Input.mousePosition))
            {
                rectTransform.anchoredPosition = dropZoneRT.anchoredPosition;
                isSnapped = true;
                Debug.Log("🎯 Fabric placed in drop zone!");
            }
            else if (isSnapped)
            {
                rectTransform.anchoredPosition = dropZoneRT.anchoredPosition;
            }
            else
            {
                rectTransform.anchoredPosition = originalPosition;
            }
        }
        else
        {
            Debug.LogError("❌ DropZone not found!");
        }
    }
}
