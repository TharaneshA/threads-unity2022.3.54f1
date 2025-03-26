using UnityEngine;
using UnityEngine.UI;

public class ScissorsButtonHandler : MonoBehaviour
{
    public Texture2D scissorsCursor;  // Assign the cursor image in the inspector
    public Vector2 cursorHotspot = Vector2.zero;  // Cursor hotspot, default at top-left

    private bool isScissorsCursorActive = false;

    public void OnScissorsButtonClick()
    {
        if (!isScissorsCursorActive)
        {
            ActivateScissorsCursor();
        }
        else
        {
            ResetCursor();  // Reset to default if clicked again
        }
    }

    private void ActivateScissorsCursor()
    {
        if (scissorsCursor == null)
        {
            Debug.LogError("❌ Scissors cursor is not assigned in the Inspector!");
            return;
        }

        Cursor.SetCursor(scissorsCursor, cursorHotspot, CursorMode.Auto);
        isScissorsCursorActive = true;
        Debug.Log("✂️ Scissors cursor activated.");
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        isScissorsCursorActive = false;
        Debug.Log("🔄 Cursor reset to default.");
    }
}
