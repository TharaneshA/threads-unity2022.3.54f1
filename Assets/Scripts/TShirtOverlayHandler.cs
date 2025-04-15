using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TShirtOverlayHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool IsTracingNow = false;

    public GameObject overlayPrefab;
    public Vector2 overlaySize = new Vector2(300f, 300f);
    public Sprite tShirtFinalSprite;

    private GameObject overlayInstance;
    private PolygonCollider2D outlineCollider;
    private bool isTracingActive = false;
    private bool mouseHeld = false;

    private float totalTraceTime = 0f;
    private float insideOutlineTime = 0f;

    private bool outlineSpawned = false;

    private void Start()
    {
        if (overlayPrefab == null)
        {
            Debug.LogError("❌ Overlay prefab not assigned!");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ScissorsButtonHandler.IsScissorsCursorActive && !outlineSpawned)
        {
            Debug.Log("✂️ First scissor click - locking fabric and showing outline.");
            ShowOverlay();
        }
    }

    private void ShowOverlay()
    {
        if (overlayInstance != null) return;

        overlayInstance = Instantiate(overlayPrefab, transform);
        RectTransform rt = overlayInstance.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.sizeDelta = overlaySize;
            rt.anchoredPosition = Vector2.zero;
        }

        outlineCollider = overlayInstance.GetComponent<PolygonCollider2D>();
        if (outlineCollider == null)
        {
            Debug.LogError("❌ PolygonCollider2D missing from outline prefab.");
        }

        isTracingActive = true;
        IsTracingNow = true;
        outlineSpawned = true;

        Debug.Log("✅ Overlay & tracing mode activated.");
    }

    private void Update()
    {
        if (!isTracingActive || outlineCollider == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseHeld = true;
            Debug.Log("🖱️ Tracing started...");
        }

        if (mouseHeld && Input.GetMouseButton(0))
        {
            TrackTracingAccuracy();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseHeld = false;
            Debug.Log("🛑 Mouse released. Accuracy check incoming.");
            CheckTracingResult();
        }
    }

    private void TrackTracingAccuracy()
    {
        totalTraceTime += Time.deltaTime;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (outlineCollider.OverlapPoint(mousePos))
        {   
            
            insideOutlineTime += Time.deltaTime;
        }
    }

    private void CheckTracingResult()
    {
        if (totalTraceTime <= 0f) return;

        float accuracy = insideOutlineTime / totalTraceTime;
        Debug.Log($"📊 Tracing Accuracy: {accuracy:P1}");

        if (accuracy >= 0.0f)
        {   
            Debug.Log("✅ Accuracy sufficient, completing tracing.");
            CompleteTracing();
        }
        else
        {   
            Debug.LogWarning("❌ Tracing accuracy too low. Try again.");
            ResetTracing();
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
            Debug.Log("🎉 T-shirt updated after accurate trace.");
        }

        // Optionally reset for future tries
        totalTraceTime = 0f;
        insideOutlineTime = 0f;
    }

    private void ResetTracing()
    {
        // Reset stats
        totalTraceTime = 0f;
        insideOutlineTime = 0f;
        // Keep tracing mode on for retry
    }
}
