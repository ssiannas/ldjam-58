using UnityEngine;

namespace ldjam_58
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private ScoreChannel scoreChannel;
        private uint _souls;

        private void Awake()
        {
            if (scoreChannel is null)
            {
                throw new MissingComponentException("ScoreChannel is not assigned in the inspector");
            }

            scoreChannel.OnScoreAdded += AddSouls;
            
            if (uiChannel is null)
            {
                throw new MissingComponentException("UIChannel is not assigned in the inspector");
            }
        }

        private void AddSouls(uint numSouls)
        {
            _souls += numSouls;
            uiChannel.UpdateScore(_souls.ToString());
        }
    }
}