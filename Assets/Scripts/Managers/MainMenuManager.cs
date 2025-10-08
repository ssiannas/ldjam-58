using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ldjam_58
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Texture2D boneCursor;
        [SerializeField] AudioChannel audioChannel;
        private void Awake()
        {
           Cursor.SetCursor(boneCursor, Vector2.zero, CursorMode.Auto); 
              audioChannel.PlayAudio("main_theme");
        }

        // This is called by the Start button
    public void StartGame()
        {
            SceneManager.LoadScene("IntroUITest");
        }
        public void QuitGame()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
