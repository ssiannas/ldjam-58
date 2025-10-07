using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (_dialogBoxController == null)
            {
                throw new MissingComponentException("DialogueBoxController component is missing from IntroUIManager GameObject");
            }
            _dialogBoxController.OnDialogueComplete += StartGame;
        }

        private void Start()
        {
            MaybeShowSequence(_introSequence, shouldKeepOnComplete: true);
        }
        
        private void MaybeShowDialogue(string id)
        {
            _dialogBoxController?.ShowTextById(id);
        }

        private void MaybeShowSequence(string[] sequence, bool shouldKeepOnComplete = false)
        {
            _dialogBoxController?.ShowDialogueSequence(sequence, shouldKeepOnComplete);
        }
        
        // Update is called once per frame
        void Update()
        {

        }

        private void StartGame()
        {
            SceneManager.LoadScene("FinalLevel");
        }

        private void OnDestroy()
        {
            if (_dialogBoxController == null) return;
            _dialogBoxController.OnDialogueComplete -= StartGame;
        }
    }
}