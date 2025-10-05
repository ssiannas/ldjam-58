using System;
using TMPro;
using UnityEngine;

namespace ldjam_58
{
    [RequireComponent(typeof(ScoreController))]
    public class GameManager : MonoBehaviour
    {
        private ScoreController _scoreController;
        [SerializeField] private GameManagerChannel channel;
        [SerializeField] private GameState gameState;
        [SerializeField] private float currentPassModifier = 0f;
        [SerializeField] private MinionController[] minions;        

        private void Awake()
        {
            _scoreController = GetComponent<ScoreController>();
            if (_scoreController is null)
            {
                Debug.LogError("ScoreManager component is missing from GameManager GameObject", this);
                return;
            }
            
            if (channel is null)
            {
                throw new MissingComponentException("GameManagerChannel is not assigned in the inspector");
            }
            channel.OnAddScoreRequested += _scoreController.AddSouls;
            channel.OnRemoveScoreRequested += _scoreController.RemoveSouls;

            channel.OnChangePassiveIncomeRequested += SetPassiveIncome;
            channel.OnSpawnMinionRequested += SpawnMinions;
            DontDestroyOnLoad(this);
            gameState.Reset();
        }

        private void SpawnMinions()
        {
            foreach (var minion in minions)
            {
                minion.EnableMinion();
            }
        }

        // Update is called once per frame
        void Update()
        {
        }


        public void SetPassiveIncome(uint passiveModifier) 
        {
            _scoreController.SetPassiveIncome(passiveModifier);
        }
    }
}
