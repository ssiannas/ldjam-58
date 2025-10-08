using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "GameState", menuName = "SO/GameState")]
    public class GameState : ScriptableObject
    {
         public float CurrentSpawnRate = 1.0f;
         public uint CurrentSouls = 1;
        public bool IsPaused = false;
         public uint CurrentSoulValue = 1;

         public ConcurrentDictionary<Upgrade.UpgradeType, int> CurrentUpgradeTier;
         public uint CurrentSpawnerTier = 0;
         
         [Header("Reset Values")]
         [SerializeField] float defaultSpawnRate = 2.0f;
         [SerializeField] uint defaultCurrentSouls = 1;
         [SerializeField] uint defaultCurrentSoulValue = 1;

         private void OnEnable()
         {
             CurrentUpgradeTier = new ConcurrentDictionary<Upgrade.UpgradeType, int>( new Dictionary<Upgrade.UpgradeType, int>() {
                 { Upgrade.UpgradeType.SpawnRate, 0 },
                 { Upgrade.UpgradeType.WeaponUpgrade, 0 },
                 { Upgrade.UpgradeType.Minions, 0 },
                 { Upgrade.UpgradeType.PassiveIncome, 0 },
                 { Upgrade.UpgradeType.SpawnerUpgrade, 0}
             });
         }
         public void Reset()
         {
             CurrentSpawnRate = defaultSpawnRate;
             CurrentSouls = defaultCurrentSouls;
             CurrentSoulValue = defaultCurrentSoulValue;
             foreach (Upgrade.UpgradeType upgradeType in Enum.GetValues(typeof(Upgrade.UpgradeType)))
             {
                 CurrentUpgradeTier[upgradeType] = 0;
             }
         }
    }
}
 