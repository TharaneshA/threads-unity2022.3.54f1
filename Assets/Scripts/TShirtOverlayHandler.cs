using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TShirtOverlayHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool IsTracingNow = false; // 🔒 Used by DraggableFabric to block dragging

    public GameObject overlayPrefab;
    public Vector2 overlaySize = new Vector2(300f, 300f);
    public Sprite tShirtFinalSprite;

    private GameObject overlayInstance;
    private bool isTracingActive = false;
    private float traceProgress = 0f;
    private bool isFabricDraggable = true;
    private bool outlineSpawned = false;
    private bool mouseHeld = false;

    private void Start()
    {
        if (overlayPrefab == null)
        {
            Debug.LogError("❌ Overlay prefab is not assigned!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScissorsButtonHandler.IsScissorsCursorActive)
        {
            if (!outlineSpawned)
            {
                Debug.Log("✂️ First scissor click - locking fabric and showing outline.");
                ShowOverlay();
            }
        }
    }

    private void ShowOverlay()
    {
        if (overlayInstance != null)
        {
            Debug.LogWarning("⚠️ Overlay already active.");
            return;
        }

        overlayInstance = Instantiate(overlayPrefab, transform);
        RectTransform rt = overlayInstance.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = overlaySize;
            rt.anchoredPosition = Vector2.zero;
        }

        isTracingActive = true;
        IsTracingNow = true;
        isFabricDraggable = false;
        outlineSpawned = true;
        Debug.Log("✅ Overlay spawned and tracing activated.");
    }

    private void Update()
    {
        if (isTracingActive && overlayInstance != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseHeld = true;
                Debug.Log("✏️ Tracing started...");
            }

            if (mouseHeld && Input.GetMouseButton(0))
            {
                TraceFabricOutline();
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseHeld = false;
                Debug.Log("🛑 Mouse released. Stopping tracing input.");
            }
        }
    }

    private void TraceFabricOutline()
    {
        traceProgress += Time.deltaTime * 10f;

        // Here, in the future, we can check if cursor is within polygon collider
        // for a more accurate tracing simulation

        if (traceProgress >= 60f)
        {
            CompleteTracing();
        }
    }

    private void CompleteTracing()
    {
        isTracingActive = false;
        IsTracingNow = false;

        if (overlayInstance != null)
        {
            Destroy(overlayInstance);
        }

        Image fabricImage = GetComponent<Image>();
        if (fabricImage != null && tShirtFinalSprite != null)
        {
            fabricImage.sprite = tShirtFinalSprite;
            Debug.Log("🎉 T-shirt fabric traced successfully!");
        }

        isFabricDraggable = true; // Now fabric can be dragged again
        Debug.Log("✅ Tracing completed. Fabric unlocked.");
    }

    public bool IsFabricDraggable()
    {
        return isFabricDraggable;
    }
}
