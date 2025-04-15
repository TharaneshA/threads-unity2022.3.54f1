using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TShirtStitchHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject stitchOverlayPrefab;     // Prefab with 6 buttons
    public GameObject finalTshirtSpritePrefab; // Final stitched shirt image/prefab

    private GameObject stitchOverlayInstance;
    private bool isStitchingStarted = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (NeedleButtonHandler.IsNeedleCursorActive && !isStitchingStarted)
        {
            Debug.Log("🪡 Needle clicked T-shirt. Starting stitching.");
            LockTShirt();
            ShowStitchOverlay();
        }
    }
    private void Start()
    {
        Debug.Log("🧵 TShirtStitchHandler is active on: " + gameObject.name);
    }
    private void LockTShirt()
    {
        DraggableFabric draggable = GetComponent<DraggableFabric>();
        if (draggable != null)
        {
            draggable.SetDragEnabled(false);
            Debug.Log("🔒 T-shirt locked for stitching.");
        }
    }

    private void ShowStitchOverlay()
    {
        if (stitchOverlayPrefab == null)
        {
            Debug.LogError("❌ Stitch overlay prefab is not assigned.");
            return;
        }

        stitchOverlayInstance = Instantiate(stitchOverlayPrefab, transform);
        stitchOverlayInstance.transform.localPosition = Vector3.zero;
        isStitchingStarted = true;
        Debug.Log("🧵 Stitch overlay spawned.");
    }

    public void OnAllDotsStitched()
    {
        if (stitchOverlayInstance != null)
            Destroy(stitchOverlayInstance);

        if (finalTshirtSpritePrefab != null)
        {
            Image tshirtImage = GetComponent<Image>();
            Image newSpriteImage = finalTshirtSpritePrefab.GetComponent<Image>();

            if (tshirtImage != null && newSpriteImage != null)
            {
                tshirtImage.sprite = newSpriteImage.sprite;
                Debug.Log("🎉 Final stitched T-shirt applied.");
            }
        }

        DraggableFabric drag = GetComponent<DraggableFabric>();
        if (drag != null)
        {
            drag.SetDragEnabled(true);
            Debug.Log("🔓 T-shirt is now draggable again.");
        }
    }
}
