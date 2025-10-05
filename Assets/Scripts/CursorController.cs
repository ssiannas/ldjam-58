using UnityEngine;

namespace ldjam_58
{
    public class CursorController : MonoBehaviour
    {
        [Header("Cursor Textures")]
        [SerializeField] private Texture2D skellyHandDefault;
        [SerializeField] private Texture2D skellyHandClicked;
        [SerializeField] private Texture2D netHandDefault;
        [SerializeField] private Texture2D netHandClicked;
        [SerializeField] private Texture2D scytheHandDefault;
        [SerializeField] private Texture2D scytheHandClicked;
        [SerializeField] private Texture2D unalivatronHandDefault;
        [SerializeField] private Texture2D unalivatronHandClicked;
        
       private Texture2D cursorTexture;
       private Texture2D cursorTextureClicked;
        
        public Vector2 hotspot = Vector2.zero;
        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (skellyHandDefault is null || 
                skellyHandClicked is null || 
                netHandDefault is null || 
                netHandClicked is null || 
                scytheHandDefault is null || 
                scytheHandClicked is null || 
                unalivatronHandDefault is null || 
                unalivatronHandClicked is null)
            {
                throw new MissingComponentException("One or more cursor textures are not assigned in the inspector");
            }
            
            cursorTexture = skellyHandDefault;
            cursorTextureClicked = skellyHandClicked;
            
            Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
        }
        
        public void OnClick()
        {
            Cursor.SetCursor(cursorTextureClicked, hotspot, CursorMode.Auto);
        }
        
        public void OnRelease()
        {
            Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
        }
        
        public void SetCursor(PlayerWeapons weapon)
        {
            switch (weapon)
            {
                case PlayerWeapons.None:
                    cursorTexture = skellyHandDefault;
                    cursorTextureClicked = skellyHandClicked;
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
                    break;
                case PlayerWeapons.Net:
                    cursorTexture = netHandDefault;
                    cursorTextureClicked = netHandClicked;
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
                    break;
                case PlayerWeapons.Scythe:
                    cursorTexture = scytheHandDefault;
                    cursorTextureClicked = scytheHandClicked;
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
                    break;
                case PlayerWeapons.Unalivatron:
                    cursorTexture = unalivatronHandDefault;
                    cursorTextureClicked = unalivatronHandClicked;
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
                    break;
                default:
                    cursorTexture = skellyHandDefault;
                    cursorTextureClicked = skellyHandClicked;
                    Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
                    break;
            }
        }
    }
}