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
        void Awake()
        {
            _dialogBoxController = GetComponentInChildren<DialogueBoxController>();
            
            uiChannel.OnShowDialog += MaybeShowDialogue;
        }

        private void Start()
        {
            MaybeShowDialogue("intro_1");
        }
        
        private void MaybeShowDialogue(string id)
        {
            _dialogBoxController?.ShowTextById(id);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}