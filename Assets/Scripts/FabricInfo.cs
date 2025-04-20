using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FabricInfo : MonoBehaviour
{
    public string fabricName;
    [TextArea] public string description;
    public bool isBiodegradable;

    public GameObject infoPanelPrefab;

    private GameObject currentInfoPanel;
    private RectTransform playerRect;
    private RectTransform fabricRect;
    public float triggerDistance = 25f;

    private bool panelShown = false;

    void Start()
    {
        fabricRect = GetComponent<RectTransform>();
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            playerRect = playerObj.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (playerRect == null || fabricRect == null) return;

        float dist = Vector2.Distance(playerRect.anchoredPosition, fabricRect.anchoredPosition);
        if (dist <= triggerDistance && !panelShown)
            ShowInfoPanel();
        else if (dist > triggerDistance && panelShown)
            HideInfoPanel();
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
    }

    void HideInfoPanel()
    {
        if (currentInfoPanel != null)
            Destroy(currentInfoPanel);
        panelShown = false;
    }

    void OnBuyFabric(){
    if (isBiodegradable)
    {
        InventoryManager.AddFabric(fabricName);
        HideInfoPanel();
    }
    else
    {
        TMP_Text warningText = currentInfoPanel.transform.Find("WarningText")?.GetComponent<TMP_Text>();

        if (warningText != null)
        {
            warningText.text = "This fabric is not biodegradable!";
            warningText.color = Color.red;
        }
        else
        {
            // If no separate WarningText, fallback to description
            TMP_Text desc = currentInfoPanel.transform.Find("DescriptionText")?.GetComponent<TMP_Text>();
            if (desc != null)
                desc.text += "\n\nThis fabric is not biodegradable!\nChoose a different fabric.";
                desc.color = Color.red;
        }

        // Optionally: don't hide panel immediately, let user see warning
        // HideInfoPanel();  // Remove this line if you want panel to stay
    }
}

}
