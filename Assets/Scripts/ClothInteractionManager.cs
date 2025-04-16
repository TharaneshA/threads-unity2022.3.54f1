using UnityEngine;
using UnityEngine.UI;

public class ClothInteractionManager : MonoBehaviour
{
    public GameObject infoPanel;
    public Text clothNameText;
    public Text clothDescriptionText;
    public Text errorText;
    public Button selectButton;

    private ClothInfo currentCloth;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right click
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                ClothInfo cloth = hit.collider.GetComponent<ClothInfo>();
                if (cloth != null)
                {
                    ShowClothInfo(cloth);
                }
            }
        }
    }

    void ShowClothInfo(ClothInfo cloth)
    {
        currentCloth = cloth;
        clothNameText.text = cloth.clothName;
        clothDescriptionText.text = cloth.description;
        errorText.text = "";
        infoPanel.SetActive(true);
    }

    public void OnSelectButtonClicked()
    {
        if (currentCloth != null)
        {
            if (currentCloth.isBiodegradable)
            {
                //PlayerInventory.AddToInventory(currentCloth.clothName);
                infoPanel.SetActive(false);
            }
            else
            {
                errorText.text = "❌ This cloth is NOT biodegradable!";
            }
        }
    }
}
