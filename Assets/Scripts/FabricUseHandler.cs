using UnityEngine;
using UnityEngine.UI;

public class FabricUseHandler : MonoBehaviour
{
    public GameObject fabricPrefab;
    private GameObject currentFabricInstance;
    public RectTransform dropZone;
    private Canvas canvas;

    public bool isScissorsActive = false;
    public bool isFabricSnapped = false;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        if (dropZone == null)
        {
            Debug.LogError("❌ Drop zone not assigned in FabricUseHandler!");
        }
    }

    public void OnUseButtonPressed(Sprite fabricSprite)
    {
        if (fabricPrefab == null || fabricSprite == null)
        {
            Debug.LogError("❌ Fabric prefab or sprite is missing!");
            return;
        }

        if (currentFabricInstance != null)
        {
            Destroy(currentFabricInstance);
        }

        currentFabricInstance = Instantiate(fabricPrefab, canvas.transform);
        Image fabricImage = currentFabricInstance.GetComponent<Image>();

        if (fabricImage != null)
        {
            fabricImage.sprite = fabricSprite;
        }

        RectTransform fabricRT = currentFabricInstance.GetComponent<RectTransform>();
        fabricRT.anchoredPosition = Vector2.zero;
        isFabricSnapped = false;
    }

    public bool IsOverDropZone()
    {
        if (dropZone == null)
        {
            Debug.LogError("❌ Drop zone is not assigned!");
            return false;
        }

        return RectTransformUtility.RectangleContainsScreenPoint(
            dropZone,
            Input.mousePosition,
            canvas.worldCamera
        );
    }

    public void SnapToDropZone(GameObject fabric)
    {
        RectTransform fabricRT = fabric.GetComponent<RectTransform>();
        fabricRT.anchoredPosition = dropZone.anchoredPosition;
        isFabricSnapped = true;
        Debug.Log("🎯 Fabric snapped to drop zone!");
    }

    public void ReturnToDropZone(GameObject fabric)
    {
        if (isFabricSnapped)
        {
            SnapToDropZone(fabric);
        }
        else
        {
            fabric.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

    public void OnFabricCut()
    {
        if (currentFabricInstance != null && isFabricSnapped)
        {
            Debug.Log("✂️ Fabric has been cut successfully!");
            // Logic to change fabric to the cut version or T-shirt sprite
        }
        else
        {
            Debug.LogWarning("⚠️ Fabric is not properly snapped or no fabric present.");
        }
    }
}
