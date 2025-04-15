using UnityEngine;
using UnityEngine.UI;

public class StitchDot : MonoBehaviour
{
    public static int stitchedCount = 0;
    public static int totalDots = 6;

    private Button button;
    private bool isStitched = false;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleStitch);
    }

    private void HandleStitch()
    {
        if (isStitched || !NeedleButtonHandler.IsNeedleCursorActive)
            return;

        isStitched = true;
        stitchedCount++;

        GetComponent<Image>().color = Color.green;

        Debug.Log("🪡 Stitched dot: " + stitchedCount + "/" + totalDots);

        if (stitchedCount >= totalDots)
        {
            TShirtStitchHandler tshirt = FindObjectOfType<TShirtStitchHandler>();
            if (tshirt != null)
            {
                tshirt.OnAllDotsStitched();
                stitchedCount = 0; // Reset for next time
            }
        }
    }
}

