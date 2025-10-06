using System.Linq;
using UnityEngine;

namespace ldjam_58.Dialogues
{
    [CreateAssetMenu(fileName = "DialogueDatabase", menuName = "SO/Dialogue/DialogueDatabase")]
    public class DialogueDatabase : ScriptableObject
    {
        public DialogueEntry[] entries;

        public DialogueEntry GetEntry(string id) { 
            return entries.FirstOrDefault(entry => entry.id == id);
        }
    }

}