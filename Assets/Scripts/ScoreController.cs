using System;
using UnityEngine;

namespace ldjam_58
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private GameState gameState;
        private uint _souls;
        private float _passiveIncomeRate = 0f;
        private float _soulsFloat = 0f;

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

        private void Update()
        {
            _soulsFloat += Time.deltaTime * _passiveIncomeRate;
            if ((uint)_soulsFloat > _souls)
            {
                SetSouls((uint)_soulsFloat);
            }
        }

        private void SetSouls(uint numSouls)
        {
            _souls = numSouls;
            _soulsFloat = numSouls;
            gameState.CurrentSouls = _souls;
            uiChannel.UpdateScore(_souls.ToString());
        }
        
        public void AddSouls(uint numSouls)
        {
            _souls += numSouls;
            _soulsFloat += numSouls;
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

        public void SetPassiveIncome(float passiveIncome)
        {
            _passiveIncomeRate = passiveIncome;
        }
    }
}