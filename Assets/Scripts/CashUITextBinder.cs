using TMPro;
using UnityEngine;

public class CashUITextBinder : MonoBehaviour
{
    public TextMeshProUGUI cashTextUI;

    void Start()
    {
        if (CashManager.instance != null)
        {
            CashManager.instance.cashText = cashTextUI;
            CashManager.instance.UpdateCashUI();
        }
    }
}

