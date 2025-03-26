using UnityEngine;
using UnityEngine.EventSystems;

public class ScissorsCutHandler : MonoBehaviour, IPointerClickHandler
{
    private FabricUseHandler fabricUseHandler;
    private int cutProgress = 0;  // Track the cutting progress
    private int requiredCuts = 5;  // Number of clicks required to cut

    private void Start()
    {
        fabricUseHandler = FindObjectOfType<FabricUseHandler>();
        if (fabricUseHandler == null)
            Debug.LogError("❌ FabricUseHandler not found!");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (fabricUseHandler == null)
        {
            Debug.LogError("❌ No fabric assigned for cutting.");
            return;
        }

        cutProgress++;
        Debug.Log("✂️ Cutting progress: " + cutProgress + "/" + requiredCuts);

        if (cutProgress >= requiredCuts)
        {
            Debug.Log("✅ Cut complete! Transforming fabric.");
            fabricUseHandler.OnFabricCut();  // Transform into T-shirt sprite
        }
    }
}
