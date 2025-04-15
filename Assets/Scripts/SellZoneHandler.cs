using UnityEngine;
using UnityEngine.EventSystems;

public class SellZoneHandler : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("📥 OnDrop Triggered!");

        GameObject dropped = eventData.pointerDrag;

        if (dropped != null)
        {
            Debug.Log("🔍 Dropped Object: " + dropped.name);

            if (dropped.CompareTag("FinalTShirt"))
            {
                Debug.Log("✅ FinalTShirt recognized. Selling now.");
                Destroy(dropped);
                CashManager.instance.AddCash(30);
            }
            else
            {
                Debug.Log("❌ Dropped object is not tagged as FinalTShirt.");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ No object was dragged.");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("🎯 Pointer entered Sell Zone");
    }

}
