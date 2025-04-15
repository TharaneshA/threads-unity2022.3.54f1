using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TShirtOverlayHandler : MonoBehaviour, IPointerClickHandler
{
    public static bool IsTracingNow = false;

    public GameObject overlayPrefab;
    public Vector2 overlaySize = new Vector2(300f, 300f);
    public Sprite tShirtFinalSprite;

    public Material dottedLineMaterial; // 🔸 Assign a dotted material in inspector
    public float requiredTraceDistance = 300f; // 🔸 Set how long user must trace

    private GameObject overlayInstance;
    private LineRenderer traceLine;
    private bool isTracingActive = false;
    private bool mouseHeld = false;
    private bool outlineSpawned = false;

    private float tracedDistance = 0f;
    private Vector3 lastPoint;
    private int pointCount = 0;

    private void Start()
    {
        if (overlayPrefab == null)
            Debug.LogError("❌ Overlay prefab not assigned!");
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

        traceLine = overlayInstance.AddComponent<LineRenderer>();
        traceLine.material = dottedLineMaterial;
        traceLine.textureMode = LineTextureMode.Tile;
        traceLine.startWidth = 0.05f;
        traceLine.endWidth = 0.05f;
        traceLine.numCapVertices = 2;
        traceLine.alignment = LineAlignment.TransformZ;
        traceLine.positionCount = 0;

        isTracingActive = true;
        IsTracingNow = true;
        outlineSpawned = true;

        Debug.Log("✅ Overlay and dotted tracing enabled.");
    }

    private void Update()
    {
        if (!isTracingActive || overlayInstance == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseHeld = true;
            tracedDistance = 0f;
            pointCount = 0;
            traceLine.positionCount = 0;

            lastPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lastPoint.z = 0;
            AddPointToTrace(lastPoint);
        }

        if (mouseHeld && Input.GetMouseButton(0))
        {
            Vector3 current = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            current.z = 0;

            float dist = Vector3.Distance(lastPoint, current);
            if (dist > 0.05f)
            {
                tracedDistance += dist;
                AddPointToTrace(current);
                lastPoint = current;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseHeld = false;
            Debug.Log($"📏 Total Traced Distance: {tracedDistance:F1}");

            if (tracedDistance >= requiredTraceDistance)
            {
                CompleteTracing();
            }
            else
            {
                Debug.LogWarning("❌ Tracing too short. Try again.");
                ResetTracing();
            }
        }
    }

    private void AddPointToTrace(Vector3 point)
    {
        pointCount++;
        traceLine.positionCount = pointCount;
        traceLine.SetPosition(pointCount - 1, point);
    }

    private void CompleteTracing()
    {
        isTracingActive = false;
        IsTracingNow = false;

        if (overlayInstance != null)
        {
            Destroy(overlayInstance);
        }

        // Find the FabricUseHandler and call its OnTracingComplete method
        FabricUseHandler fabricHandler = FindObjectOfType<FabricUseHandler>();
        if (fabricHandler != null)
        {
            fabricHandler.OnTracingComplete();
            Debug.Log("🎉 Notified FabricUseHandler to spawn final t-shirt.");
        }
        else
        {
            Debug.LogError("❌ Cannot find FabricUseHandler in the scene!");

            // Fallback to the original behavior if FabricUseHandler cannot be found
            Image fabricImage = GetComponent<Image>();
            if (fabricImage != null && tShirtFinalSprite != null)
            {
                fabricImage.sprite = tShirtFinalSprite;
                Debug.Log("🎉 T-shirt updated after accurate trace (fallback method).");
            }
        }
    }
    private void ResetTracing()
    {
        tracedDistance = 0f;
        pointCount = 0;
        if (traceLine != null)
            traceLine.positionCount = 0;
        Debug.Log("🔁 Reset tracing path.");
    }
}
