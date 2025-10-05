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
      public UnityAction<PlayerWeapons> OnChangePlayerWeaponRequested;
      public UnityAction<uint> OnChangePassiveIncomeRequested;

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
      
      public void ChangePlayerWeapon(PlayerWeapons newWeapon)
      {
         OnChangePlayerWeaponRequested?.Invoke(newWeapon);  
      }

        public void ChangePassiveIncome(uint incomeModifier)
        {
            OnChangePassiveIncomeRequested?.Invoke(incomeModifier);
        }

    }
}