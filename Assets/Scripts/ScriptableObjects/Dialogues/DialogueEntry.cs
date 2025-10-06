using UnityEngine;

namespace ldjam_58.Dialogues
{
    [CreateAssetMenu(fileName = "DialogueEntry", menuName = "SO/Dialogue/DialogueEntry")]
    [System.Serializable]
    public class DialogueEntry : ScriptableObject
    {
        public string id;
        public string text;
        public string characterName;
        // Play on dialogue show?
        public AudioClip voiceClip;
    }
}