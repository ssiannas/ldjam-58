using UnityEngine;

namespace ldjam_58
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        private uint _souls;

        private void Start()
        {
            if (uiChannel is null)
            {
                throw new MissingComponentException("UIChannel is not assigned in the inspector");
            }

            SetSouls(1); // Start at 1 soul
        }
    
        private void SetSouls(uint numSouls)
        {
            _souls = numSouls;
            uiChannel.UpdateScore(_souls.ToString());
        }
        
        public void AddSouls(uint numSouls)
        {
            _souls += numSouls;
            uiChannel.UpdateScore(_souls.ToString());
        }
    }
}