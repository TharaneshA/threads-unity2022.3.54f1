using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FabricButton : MonoBehaviour
{
    public Sprite fabricImage; // Full fabric image to drag
    private GameObject fabricDrag; // Reference to the draggable fabric object
    private RectTransform fabricDragRect;

    private void Start()
    {
        // Find the FabricDrag object in the scene
        fabricDrag = GameObject.Find("FabricDrag");

        if (fabricDrag == null)
        {
            Debug.LogError("FabricDrag not found! Make sure it's named correctly.");
        }
        else
        {
            fabricDragRect = fabricDrag.GetComponent<RectTransform>();
            fabricDrag.SetActive(false); // Hide initially
        }
    }

    // Called when Use Button is clicked
    public void OnUseButtonClick()
    {
        if (fabricDrag != null)
        {
            // Enable the draggable fabric and set its sprite
            fabricDrag.SetActive(true);
            fabricDrag.GetComponent<Image>().sprite = fabricImage;

            // Start dragging from current mouse position
            StartDrag();
        }
    }

    private void StartDrag()
    {
        if (fabricDrag != null)
        {
            // Move fabric to mouse position
            Vector2 mousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                fabricDrag.transform.parent as RectTransform,
                Input.mousePosition,
                Camera.main,
                out mousePos
            );
            fabricDragRect.anchoredPosition = mousePos;
        }
    }

    private void Update()
    {
        if (fabricDrag != null && fabricDrag.activeSelf)
        {
            DragFabric();
            if (Input.GetMouseButtonDown(0)) // Left-click to drop
            {
                DropFabric();
            }
        }
    }

    private void DragFabric()
    {
        Vector2 mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            fabricDrag.transform.parent as RectTransform,
            Input.mousePosition,
            Camera.main,
            out mousePos
        );
        fabricDragRect.anchoredPosition = mousePos;
    }

    private void DropFabric()
    {
        // Stop dragging and disable fabric if dropped
        fabricDrag.SetActive(false);
    }
}
