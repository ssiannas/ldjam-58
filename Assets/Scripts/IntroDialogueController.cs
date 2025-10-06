using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

namespace ldjam_58
{
    public class IntroDialogueController : MonoBehaviour
    {

        [Header("References")]
        [SerializeField] private VideoPlayer videoPlayer;
        //[SerializeField] private DialogueUI dialogueUI; // your existing text UI
        [SerializeField] private string nextSceneName = "MainMenu";
        [SerializeField] private UIChannel uiChannel;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            PlayIntroSequence();
        }

        // Update is called once per frame
        void Update()
        {

        }



        private IEnumerator PlayIntroSequence()
        {

            Debug.Log("milapousti");
            yield return new WaitForSeconds(1f);
            uiChannel.ShowDialog("intro_0");
            yield return new WaitForSeconds(3f);
            Debug.Log("gamisou");
            
            yield return new WaitForSeconds(3f);
            uiChannel.ShowDialog("intro_2");
            yield return new WaitForSeconds(4f);
            uiChannel.ShowDialog("intro_3");
            yield return new WaitForSeconds(0.5f);



            //SceneManager.LoadScene(nextSceneName);
        }
    }
}
