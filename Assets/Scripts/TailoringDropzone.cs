using UnityEngine;
using UnityEngine.EventSystems;

public class TailoringDropzone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform droppedObject = eventData.pointerDrag.GetComponent<RectTransform>();

            if (droppedObject != null)
            {
                droppedObject.SetParent(transform);  // Parent to the dropzone
                droppedObject.anchoredPosition = Vector2.zero; // Snap to center
                Debug.Log("Fabric snapped to Tailoring Zone!");
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }

}

