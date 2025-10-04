using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
   [CreateAssetMenu(fileName = "ScoreChannel", menuName = "SO/Channels/ScoreChannel")]
   public class UIChannel : ScriptableObject
   {
      public UnityAction<string> OnUpdateScore;

      public void UpdateScore(string newScore)
      {
         OnUpdateScore?.Invoke(newScore);
      }
   }
}