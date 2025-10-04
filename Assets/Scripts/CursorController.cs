using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D cursorTexture;
    public Vector2 hotspot = Vector2.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
