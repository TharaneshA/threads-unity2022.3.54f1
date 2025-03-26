using UnityEngine;
using UnityEngine.EventSystems;

public class FabricDropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("✅ Fabric dropped!");

        if (eventData.pointerDrag != null)
        {
            RectTransform fabricRT = eventData.pointerDrag.GetComponent<RectTransform>();
            fabricRT.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("🎯 Fabric snapped to drop zone!");
        }
    }
}
