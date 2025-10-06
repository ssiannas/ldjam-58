using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ldjam_58
{
    [RequireComponent(typeof(ScoreController), typeof(SpawnerMasterController))]
    public class GameManager : MonoBehaviour
    {
        private ScoreController _scoreController;
        [SerializeField] private GameManagerChannel channel;
        [SerializeField] private GameState gameState;
        [SerializeField] private float currentPassModifier = 0f;
        [SerializeField] private MinionController[] minions;
        [SerializeField] private SpawnerController[] spawners;
        private SpawnerMasterController _spawnerMasterController;
        [SerializeField] private UIChannel uiChannel;
        
        private void Awake()
        {
            _spawnerMasterController = GetComponent<SpawnerMasterController>();
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


            channel.OnSpawnerUpgradeRequested += UpgradeSpawner;
            channel.OnUpgradeSoulValueRequested += UpgradeSoulValue;
            
            channel.OnPauseRequested += Pause;
            channel.OnResumeRequested += Resume;
            
            channel.ResetUpgrades();
            gameState.Reset();
            
            DontDestroyOnLoad(this);

        }

        private void SpawnMinions()
        {
            foreach (var minion in minions)
            {
                minion.EnableMinion();
            }
        }


        private void UpgradeSpawner(uint newTier)
        {
            _spawnerMasterController.UnlockNextSpawner(newTier);
        }

        //float timer = 0f;
        //bool flag = true;
        // Update is called once per frame
        void Update()
        {
            //timer += Time.deltaTime;
            //if(timer>2.5f && flag)
            //{
            //    uiChannel.ShowDialog("test");
            //    flag = false;
            //}
        }


        private void Start()
        {
        }

        public void SetPassiveIncome(uint passiveModifier) 
        {
            _scoreController.SetPassiveIncome(passiveModifier);
        }


        public void Pause()
        {
            Time.timeScale = 0f;          
            gameState.IsPaused = true;

            //set all animations to idle
        }

        public void Resume()
        {
            Time.timeScale = 1f;          
            gameState.IsPaused = false;

            //resume all animators
        }

       

        private void UpgradeSoulValue(uint newSoulValue)
        {
            gameState.CurrentSoulValue = newSoulValue;
        }

    }
}
