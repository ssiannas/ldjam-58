using UnityEngine;

namespace ldjam_58
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        private uint _souls;

        private void Awake()
        {
        }

        public void AddSouls(uint numSouls)
        {
            _souls += numSouls;
            uiChannel.UpdateScore(_souls.ToString());
        }
    }
}