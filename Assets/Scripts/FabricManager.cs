using UnityEngine;
using UnityEngine.EventSystems;

public class FabricManager : MonoBehaviour
{
    public static FabricManager instance;
    public GameObject chain;
    public GameObject inventoryPanel;
    private bool isInventoryOpen = false;
    private Vector3 originalPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        originalPosition = chain.transform.position;
        CloseInventory();
    }

    public void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    private void OpenInventory()
    {
        chain.GetComponent<Animator>().SetTrigger("Click");
        isInventoryOpen = true;
    }

    private void CloseInventory()
    {
        chain.GetComponent<Animator>().SetTrigger("Reset");
        isInventoryOpen = false;
    }

    public void SetCursorToFabric(Texture2D fabricTexture)
    {
        Cursor.SetCursor(fabricTexture, Vector2.zero, CursorMode.Auto);
        CloseInventory();
    }
}
