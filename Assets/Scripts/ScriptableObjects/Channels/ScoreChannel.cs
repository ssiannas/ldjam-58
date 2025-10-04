using UnityEngine;
using UnityEngine.Events;

namespace ldjam_58
{
    [CreateAssetMenu(fileName = "ScoreChannel", menuName = "SO/Channels/ScoreChannel")]
    public class ScoreChannel : ScriptableObject
    {
        public UnityAction<uint> OnScoreAdded;

        public void AddScore(uint score)
        {
            OnScoreAdded?.Invoke(score);
        }
    }
}
