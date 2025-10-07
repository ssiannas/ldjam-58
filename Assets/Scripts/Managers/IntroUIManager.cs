using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ldjam_58
{

    public class IntroUIManager : MonoBehaviour
    {
        [Header("Channels")]
        [SerializeField] private UIChannel uiChannel;
        private DialogueBoxController _dialogBoxController;
        private readonly string[] _introSequence = {"intro_1", "intro_2", "intro_3", "intro_4"};
        void Awake()
        {
            _dialogBoxController = GetComponentInChildren<DialogueBoxController>();
            
            uiChannel.OnShowDialog += MaybeShowDialogue;
        }

        private void Start()
        {
            MaybeShowSequence(_introSequence);
    }
        
        private void MaybeShowDialogue(string id)
        {
            _dialogBoxController?.ShowTextById(id);
        }

        private void MaybeShowSequence(string[] sequence)
        {
            _dialogBoxController?.ShowDialogueSequence(sequence);
        }
        
        // Update is called once per frame
        void Update()
        {

        }
    }
}