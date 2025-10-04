using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScoreChannel", menuName = "SO/Channels/ScoreChannel")]
public class ScoreChannel : ScriptableObject
{
   public UnityAction<float> OnUpdateScore;
   
   public void UpdateScore(float score)
   { 
      OnUpdateScore?.Invoke(score);
   }
}
