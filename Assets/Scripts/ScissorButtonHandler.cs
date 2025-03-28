using UnityEngine;

public class ScissorsButtonHandler : MonoBehaviour
{
    public Texture2D scissorsCursor;
    public Vector2 cursorHotspot = Vector2.zero;

    public static bool IsScissorsCursorActive { get; private set; } = false;

    public void OnScissorsButtonClick()
    {
        if (!IsScissorsCursorActive)
        {
            ActivateScissorsCursor();
        }
        else
        {
            ResetCursor();
        }
    }

    private void ActivateScissorsCursor()
    {
        if (scissorsCursor == null)
        {
            Debug.LogError("❌ Scissors cursor is not assigned!");
            return;
        }

        Cursor.SetCursor(scissorsCursor, cursorHotspot, CursorMode.Auto);
        IsScissorsCursorActive = true;
        Debug.Log("✂️ Scissors cursor activated.");
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        IsScissorsCursorActive = false;
        Debug.Log("🔄 Cursor reset.");
    }
}
