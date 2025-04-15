using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    public TextMeshProUGUI cashText;
    private int currentCash = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateCashUI();
    }

    public void AddCash(int amount)
    {
        currentCash += amount;
        UpdateCashUI();
    }

    private void UpdateCashUI()
    {
        cashText.text = "$" + currentCash.ToString();
    }
}
