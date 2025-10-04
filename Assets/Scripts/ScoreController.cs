using UnityEngine;

namespace ldjam_58
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private GameState gameState;
        private uint _souls;

        private void Start()
        {
            if (uiChannel is null)
            {
                throw new MissingComponentException("UIChannel is not assigned in the inspector");
            }
            
            if (gameState is null)
            {
                throw new MissingComponentException("GameState is not assigned in the inspector");
            }

            SetSouls(1); // Start at 1 soul
        }
    
        private void SetSouls(uint numSouls)
        {
            _souls = numSouls;
            gameState.CurrentSouls = _souls;
            uiChannel.UpdateScore(_souls.ToString());
        }
        
        public void AddSouls(uint numSouls)
        {
            _souls += numSouls;
            gameState.CurrentSouls = _souls;
            uiChannel.UpdateScore(_souls.ToString());
        }
        
        public void RemoveSouls(uint numSouls)
        {
            if (numSouls > _souls)
            {
                SetSouls(0);
                return;
            }
            _souls -= numSouls;
            gameState.CurrentSouls = _souls;
            uiChannel.UpdateScore(_souls.ToString());
        }
    }
}