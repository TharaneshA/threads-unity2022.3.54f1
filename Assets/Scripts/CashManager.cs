using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    public TextMeshProUGUI cashText;
    private int currentCash = 30;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ✅ Persist between scenes
        }
        else
        {
            Destroy(gameObject); // ✅ Prevent duplicates
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

    public void UpdateCashUI()
    {
        if (cashText != null)
            cashText.text = "$" + currentCash.ToString();
    }
}
