using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableFabric : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private FabricUseHandler fabricHandler;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        fabricHandler = FindObjectOfType<FabricUseHandler>();
        if (fabricHandler == null)
        {
            Debug.LogError("❌ FabricUseHandler not found!");
        }

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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

        if (fabricHandler.IsOverDropZone())
        {
            fabricHandler.SnapToDropZone(gameObject);
        }
        else if (fabricHandler.isFabricSnapped)
        {
            fabricHandler.ReturnToDropZone(gameObject);
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
