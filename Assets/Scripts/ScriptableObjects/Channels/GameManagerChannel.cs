using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
   [CreateAssetMenu(fileName = "GameManagerChannel", menuName = "SO/Channels/GamerManagerChannel")]
   public class GameManagerChannel : ScriptableObject
   {
      public UnityAction OnGameStartRequested;
      public UnityAction OnGameOverRequested;
      public UnityAction OnGameRestartRequested;
      public UnityAction<uint> OnAddScoreRequested;
      public UnityAction<uint> OnRemoveScoreRequested;
      public UnityAction<Weapon> OnChangePlayerWeaponRequested;
      public UnityAction<uint> OnChangePassiveIncomeRequested;
      public UnityAction OnSpawnMinionRequested;
      public UnityAction<uint> OnSpawnerUpgradeRequested;
      public UnityAction OnUpgradeResetRequested;
      public UnityAction<uint> OnUpgradeSoulValueRequested;
      
      public void StartGame()
      {
         OnGameStartRequested?.Invoke();
      }

      public void GameOver()
      {
         OnGameOverRequested?.Invoke();
      }

      public void RestartGame()
      {
         OnGameRestartRequested?.Invoke();
      }
      
      public void AddScore(uint score)
      {
         OnAddScoreRequested?.Invoke(score);
      }
      
      public void RemoveScore(uint score)
      {
         OnRemoveScoreRequested?.Invoke(score);
      }
      
      public void ChangePlayerWeapon(Weapon newWeapon)
      {
         OnChangePlayerWeaponRequested?.Invoke(newWeapon);  
      }

      public void ChangePassiveIncome(uint incomeModifier)
      {
         OnChangePassiveIncomeRequested?.Invoke(incomeModifier);
      }
      
      public void SpawnMinion()
      {
         OnSpawnMinionRequested?.Invoke();
      }
      
      public void UpgradeSpawner(uint newTier)
      {
         OnSpawnerUpgradeRequested?.Invoke(newTier);
      }

      public void ResetUpgrades()
      {
         OnUpgradeResetRequested?.Invoke();
      }
      
      public void UpgradeSoulValue(uint newSoulValue)
      {
         OnUpgradeSoulValueRequested?.Invoke(newSoulValue);
      }
    }
}