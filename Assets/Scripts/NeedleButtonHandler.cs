using UnityEngine;

public class NeedleButtonHandler : MonoBehaviour
{
    public Texture2D needleCursor;
    public Vector2 cursorHotspot = Vector2.zero;

    public static bool IsNeedleCursorActive { get; private set; } = false;

    public void OnNeedleButtonClick()
    {
        if (!IsNeedleCursorActive)
        {
            ActivateNeedleCursor();
        }
        else
        {
            ResetCursor();
        }
    }

    private void ActivateNeedleCursor()
    {
        if (needleCursor == null)
        {
            Debug.LogError("❌ Needle cursor is not assigned!");
            return;
        }

        Cursor.SetCursor(needleCursor, cursorHotspot, CursorMode.Auto);
        IsNeedleCursorActive = true;
        Debug.Log("✂️ Needle cursor activated.");
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        IsNeedleCursorActive = false;
        Debug.Log("🔄 Cursor reset.");
    }
}


