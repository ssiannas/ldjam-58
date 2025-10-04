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
   }
}