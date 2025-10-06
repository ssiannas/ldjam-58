using System.Collections;
using ldjam_58.Dialogues;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace ldjam_58
{
    public class DialogueBoxController : MonoBehaviour
    {
        [Header("UI References")] [SerializeField]
        private RectTransform boxRect;

        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private TextMeshProUGUI characterNameText;

        [Header("Settings")] [SerializeField] private DialogueDatabase database;
        [SerializeField] private float lettersPerSecond = 20f;
        [SerializeField] private Vector2 padding = new(20f, 20f);
        [SerializeField] private float minHeight = 100f;
        [SerializeField] private float maxHeight = 400f;

        private Coroutine _typingCoroutine;
        private string _currentText;

        private bool _isTyping;

        // We could play dialogue audio clips if needed
        // Or something for on-dialogue sfx... we'll see
        private AudioSource _audioSource;
        [SerializeField] private GameManagerChannel gameManagerChannel;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            if (gameManagerChannel is null)
            {
                throw new MissingComponentException("GameManagerChannel is not assigned in the inspector");
            }
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void ShowText(string text)
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }

            _currentText = text;
            gameObject.SetActive(true);

            if (characterNameText != null && characterNameText.text != "")
                characterNameText.gameObject.SetActive(true);

            _typingCoroutine = StartCoroutine(TypeText());
        }

        private void ShowText(DialogueEntry entry)
        {
            if (entry == null)
            {
                throw new System.ArgumentNullException(nameof(entry), "DialogueEntry cannot be null");
            }

            characterNameText.text = characterNameText != null ? entry.characterName : "";
            
            ShowText(entry.text);

            if (entry.voiceClip == null || _audioSource == null) return;
            _audioSource.clip = entry.voiceClip;
            _audioSource.Play();

        }

        public void ShowTextById(string id)
        {
            if (database == null)
            {
                throw new MissingComponentException("DialogueDatabase is not assigned in the inspector");
            }

            var entry = database.GetEntry(id);
            if (entry != null)
            {
                ShowText(entry);
            }
        }

        private IEnumerator TypeText()
        {
            _isTyping = true;
            dialogText.text = "";

            float delay = 1f / lettersPerSecond;

            foreach (char letter in _currentText)
            {
                dialogText.text += letter;
                UpdateBoxSize();
                yield return new WaitForSecondsRealtime(delay);
            }

            _isTyping = false;
        }

        private void UpdateBoxSize()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(dialogText.rectTransform);
            var textHeight = dialogText.preferredHeight;
            var newHeight = Mathf.Clamp(textHeight + padding.y * 2, minHeight, maxHeight);
            boxRect.sizeDelta = new Vector2(boxRect.sizeDelta.x, newHeight);
        }

        private void Update()
        {
            bool inputPressed = (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) ||
                                (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame);

            if (!inputPressed) return;
            if (_isTyping)
            {
                CompleteText();
            }
            else
            {
                Hide();
            }
        }

        public void CompleteText()
        {
            if (_isTyping && _typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
                dialogText.text = _currentText;
                UpdateBoxSize();
                _isTyping = false;
            }
        }

        public void Hide()
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }

            if (_audioSource != null && _audioSource.isPlaying)
            {
                _audioSource.Stop();
            }
            gameManagerChannel.ResumeGame();
            gameObject.SetActive(false);
        }

        public bool IsTyping()
        {
            return _isTyping;
        }

        public void SetTypingSpeed(float speed)
        {
            lettersPerSecond = speed;
        }
    }
}