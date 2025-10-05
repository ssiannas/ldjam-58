using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "GameState", menuName = "SO/GameState")]
    public class GameState : ScriptableObject
    {
         public float SpawnRate = 1.0f;
         public uint CurrentSouls = 1;
        public bool IsPaused = false;
         public Dictionary<Upgrade.UpgradeType, int> CurrentUpgradeTier = new Dictionary<Upgrade.UpgradeType, int>()
         {
             { Upgrade.UpgradeType.SpawnRate, 0 },
             { Upgrade.UpgradeType.WeaponUpgrade, 0 },
             { Upgrade.UpgradeType.Minions, 0 },
             { Upgrade.UpgradeType.PassiveIncome, 0 },
         };
         
         [Header("Reset Values")]
         [SerializeField] float defaultSpawnRate = 2.0f;
         [SerializeField] uint defaultCurrentSouls = 1;
         
         public void Reset()
         {
             SpawnRate = defaultSpawnRate;
             CurrentSouls = defaultCurrentSouls;

             foreach (Upgrade.UpgradeType upgradeType in Enum.GetValues(typeof(Upgrade.UpgradeType)))
             {
                 CurrentUpgradeTier[upgradeType] = 0;
             }
         }
    }
}
 