using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
   [CreateAssetMenu(fileName = "UIChannel", menuName = "SO/Channels/UIChannel")]
   public class UIChannel : ScriptableObject
   {
      public UnityAction<string> OnUpdateScore;

      public void UpdateScore(string newScore)
      {
         OnUpdateScore?.Invoke(newScore);
      }
   }
}