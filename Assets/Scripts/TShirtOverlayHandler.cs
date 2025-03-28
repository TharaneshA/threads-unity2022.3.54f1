using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TShirtOverlayHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject overlayPrefab;  // T-shirt outline prefab
    public Vector2 overlaySize = new Vector2(300f, 300f);  // Default size
    public Sprite tShirtFinalSprite;  // Final T-shirt sprite after tracing
    private GameObject overlayInstance;
    private bool isTracingActive = false;
    private float traceProgress = 0f;
    private bool isFabricDraggable = true;

    private void Start()
    {
        if (overlayPrefab == null)
        {
            Debug.LogError("❌ Overlay prefab is not assigned!");
            return;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScissorsButtonHandler.IsScissorsCursorActive)
        {
            Debug.Log("✂️ Scissors clicked on fabric. Showing overlay.");
            ShowOverlay();
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
            rt.sizeDelta = overlaySize;  // Set overlay size dynamically
            rt.anchoredPosition = Vector2.zero;
        }

        isTracingActive = true;
        isFabricDraggable = false;
        Debug.Log("✅ Overlay displayed. Tracing started!");
    }

    private void Update()
    {
        if (isTracingActive)
        {
            TraceFabricOutline();
        }
    }

    private void TraceFabricOutline()
    {
        // Simulate tracing progress (for now, increase over time)
        traceProgress += Time.deltaTime * 10f;  // Adjust speed if needed

        if (traceProgress >= 60f)
        {
            CompleteTracing();
        }
    }

    private void CompleteTracing()
    {
        isTracingActive = false;

        if (overlayInstance != null)
        {
            Destroy(overlayInstance);
        }

        // ✅ Change to TShirtFinal sprite after tracing
        Image fabricImage = GetComponent<Image>();
        if (fabricImage != null && tShirtFinalSprite != null)
        {
            fabricImage.sprite = tShirtFinalSprite;
            Debug.Log("🎉 T-shirt fabric traced successfully!");
        }

        isFabricDraggable = true;  // Re-enable drag after cutting
    }

    public bool IsFabricDraggable()
    {
        return isFabricDraggable;
    }
}
