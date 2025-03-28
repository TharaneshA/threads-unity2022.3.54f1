using UnityEngine;

public class TShirtTracer : MonoBehaviour
{
    public FabricUseHandler fabricHandler;
    public float tracingThreshold = 0.6f;  // 60% completion to switch
    private float tracedLength = 0f;
    private float totalLength = 0f;
    private bool isTracingActive = false;
    private bool isTracingComplete = false;

    private LineRenderer lineRenderer;
    private Vector2 lastPoint;

    private void Start()
    {
        // Calculate total length of collider
        PolygonCollider2D polyCollider = GetComponent<PolygonCollider2D>();
        if (polyCollider != null)
        {
            totalLength = CalculateColliderLength(polyCollider);
            Debug.Log("🖊️ Total Outline Length: " + totalLength);
        }
        else
        {
            Debug.LogError("❌ No Polygon Collider found on overlay!");
        }

        // Add LineRenderer to draw traced path
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.positionCount = 0;
    }

    public void ActivateTracing()
    {
        isTracingActive = true;
        tracedLength = 0f;
        Debug.Log("✂️ Tracing initiated.");
    }

    private void Update()
    {
        if (!isTracingActive || isTracingComplete) return;

        // Check for tracing with mouse or touch
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (lastPoint != mousePos)
            {
                AddPointToLine(mousePos);
                lastPoint = mousePos;

                CheckCollision(mousePos);
            }
        }
    }

    private void AddPointToLine(Vector2 point)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }

    private void CheckCollision(Vector2 point)
    {
        Collider2D hit = Physics2D.OverlapPoint(point);
        if (hit != null && hit.gameObject == gameObject)
        {
            tracedLength += Vector2.Distance(lastPoint, point);
            float progress = tracedLength / totalLength;
            Debug.Log("📏 Tracing Progress: " + (progress * 100).ToString("F2") + "%");

            if (progress >= tracingThreshold)
            {
                CompleteTracing();
            }
        }
    }

    private void CompleteTracing()
    {
        if (isTracingComplete) return;

        isTracingComplete = true;
        isTracingActive = false;
        Debug.Log("✅ Tracing complete. Transforming to T-shirt.");
        fabricHandler.OnTracingComplete();
    }

    private float CalculateColliderLength(PolygonCollider2D polyCollider)
    {
        float length = 0f;
        Vector2[] points = polyCollider.points;
        for (int i = 0; i < points.Length - 1; i++)
        {
            length += Vector2.Distance(points[i], points[i + 1]);
        }
        length += Vector2.Distance(points[points.Length - 1], points[0]);  // Close loop
        return length;
    }
}
