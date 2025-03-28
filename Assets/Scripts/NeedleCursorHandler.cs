using UnityEngine;
using UnityEngine.UI;

public class NeedleButtonHandler : MonoBehaviour
{
    public Texture2D needleCursor;  // Assign the needle cursor in the Inspector
    public Vector2 cursorHotspot = Vector2.zero;  // Hotspot for the cursor
    public float cursorWidth = 32f;  // Default cursor width
    public float cursorHeight = 32f;  // Default cursor height

    private bool isNeedleCursorActive = false;

    public void OnNeedleButtonClick()
    {
        if (!isNeedleCursorActive)
        {
            ActivateNeedleCursor();
        }
        else
        {
            ResetCursor();  // Reset to default if clicked again
        }
    }

    private void ActivateNeedleCursor()
    {
        if (needleCursor == null)
        {
            Debug.LogError("❌ Needle cursor is not assigned in the Inspector!");
            return;
        }

        Texture2D resizedCursor = ResizeCursor(needleCursor, (int)cursorWidth, (int)cursorHeight);
        Cursor.SetCursor(resizedCursor, cursorHotspot, CursorMode.Auto);
        isNeedleCursorActive = true;
        Debug.Log("🪡 Needle cursor activated.");
    }

    private void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        isNeedleCursorActive = false;
        Debug.Log("🔄 Cursor reset to default.");
    }

    private Texture2D ResizeCursor(Texture2D source, int width, int height)
    {
        if (source == null)
        {
            Debug.LogError("❌ Cursor texture is missing.");
            return null;
        }

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        rt.filterMode = FilterMode.Bilinear;

        RenderTexture.active = rt;
        Graphics.Blit(source, rt);

        Texture2D result = new Texture2D(width, height, TextureFormat.RGBA32, false);
        result.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        result.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }
}
