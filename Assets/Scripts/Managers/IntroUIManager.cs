using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ldjam_58
{

    public class IntroUIManager : MonoBehaviour
    {

        private float _timer;
        
        [Header("Channels")]
        [SerializeField] private UIChannel uiChannel;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        private Canvas _canvas;

        private Vector2Int _cachedResolution;
        private DialogueBoxController _dialogBoxController;
        void Awake()
        {
            _dialogBoxController = GetComponentInChildren<DialogueBoxController>();
            
            uiChannel.OnGetCanvasScaleFactor += GetCanvasScaleFactor;
            uiChannel.OnShowDialog += MaybeShowDialogue;
            
            _canvas = GetComponent<Canvas>();
            _cachedResolution = new Vector2Int(Screen.width, Screen.height);
            //SetUpgradesText();
        }

        private void MaybeShowDialogue(string id)
        {
            gameManagerChannel.PauseGame();
            _dialogBoxController?.ShowTextById(id);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private float GetCanvasScaleFactor()
        {
            Canvas.ForceUpdateCanvases();
            return _canvas.scaleFactor;
        }
    }
}