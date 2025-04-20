using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    public TextMeshProUGUI cashText;
    private int currentCash = 30; // Starting cash, set as needed

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
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

    public void DeductCash(int amount)
    {
        currentCash -= amount;
        if (currentCash < 0) currentCash = 0;
        UpdateCashUI();
    }

    public void UpdateCashUI()
    {
        if (cashText != null)
            cashText.text = "$" + currentCash.ToString();
    }

    public int GetCash()
    {
        return currentCash;
    }
}
