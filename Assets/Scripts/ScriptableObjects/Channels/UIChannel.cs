using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
   [CreateAssetMenu(fileName = "UIChannel", menuName = "SO/Channels/UIChannel")]
   public class UIChannel : ScriptableObject
   {
      public UnityAction<string> OnUpdateScore;
      public UnityAction OnUpgradeAvailable;
      public UnityAction OnUpgradeReset;

      public void UpdateScore(string newScore)
      {
         OnUpdateScore?.Invoke(newScore);
      }
      
      public void NotifyUpgradeAvailable()
      {
         OnUpgradeAvailable?.Invoke();
      }
      
      public void NotifyUpgradeReset()
      {
         OnUpgradeReset?.Invoke();
      }
   }
}