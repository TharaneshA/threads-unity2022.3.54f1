using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FabricInfoUI : MonoBehaviour
{
    [Header("Fabric Details")]
    public string fabricName;
    [TextArea] public string description;
    public bool isBiodegradable;

    [Header("UI")]
    public GameObject infoPanelPrefab;

    private GameObject currentInfoPanel;
    private RectTransform playerRect;
    private RectTransform fabricRect;
    public float triggerDistance = 25f; // adjust based on canvas scale

    private bool panelShown = false;

    void Start()
    {
        fabricRect = GetComponent<RectTransform>();

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerRect = playerObj.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("❌ Player with tag 'Player' not found.");
        }
    }

    void Update()
    {
        if (playerRect == null || fabricRect == null) return;

        float dist = Vector2.Distance(playerRect.anchoredPosition, fabricRect.anchoredPosition);

        if (dist <= triggerDistance && !panelShown)
        {
            ShowInfoPanel();
        }
        else if (dist > triggerDistance && panelShown)
        {
            HideInfoPanel();
        }
    }

    void ShowInfoPanel()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null || infoPanelPrefab == null) return;

        currentInfoPanel = Instantiate(infoPanelPrefab, canvas.transform);

        TMP_Text nameText = currentInfoPanel.transform.Find("FabricNameText")?.GetComponent<TMP_Text>();
        TMP_Text descText = currentInfoPanel.transform.Find("DescriptionText")?.GetComponent<TMP_Text>();
        Button buyButton = currentInfoPanel.transform.Find("BuyButton")?.GetComponent<Button>();

        if (nameText != null) nameText.text = fabricName;
        if (descText != null) descText.text = description;

        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyFabric);
        }

        panelShown = true;
        Debug.Log("🧾 Info panel shown for: " + fabricName);
    }

    void HideInfoPanel()
    {
        if (currentInfoPanel != null)
        {
            Destroy(currentInfoPanel);
        }
        panelShown = false;
        Debug.Log("❌ Info panel hidden for: " + fabricName);
    }

    void OnBuyFabric()
    {
        if (isBiodegradable)
        {
            InventoryManager.AddFabric(fabricName);
            Debug.Log($"✅ Bought {fabricName} (biodegradable) - added to inventory.");
        }
        else
        {
            Debug.LogWarning($"⚠️ {fabricName} is not biodegradable. Consider alternatives.");
        }

        HideInfoPanel();
    }
}
