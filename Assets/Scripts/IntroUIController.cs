using UnityEngine;

namespace ldjam_58
{
    public class IntroUIController : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
             uiChannel.ShowDialog("intro_1");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
