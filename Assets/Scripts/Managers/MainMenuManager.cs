using UnityEngine;
using UnityEngine.SceneManagement;

namespace ldjam_58
{
    public class MainMenuManager : MonoBehaviour
    {

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
