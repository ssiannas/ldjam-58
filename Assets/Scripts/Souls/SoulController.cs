using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ldjam_58
{
        
    using MovementStyle = SoulMovementController.SoulMovementStyle; 
    [RequireComponent(typeof(SoulMovementController))]
    public class SoulController : MonoBehaviour
    {
        public enum SoulType
        {
            Walker,
            Flyer
        };
        
        [SerializeField] private GameState gameState;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        private MovementStyle _movementStyle = MovementStyle.Runner;
        private SpriteRenderer _spriteRenderer;
        private SoulMovementController _soulMovementController;
        
        // duck doooooooooooodgers

        void Start() {
            

        }

        private void Awake()
        {
            if (gameState is null)
            {
                throw new MissingComponentException("GameState is not assigned in the inspector");
            }
            if (gameManagerChannel is null)
            {
                throw new MissingComponentException("GameManagerChannel is not assigned in the inspector");
            }
            _movementStyle = GetRandomMovementStyle();
            _soulMovementController = GetComponent<SoulMovementController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(SoulType soulType)
        {
            _soulMovementController.Init(soulType);
        }

        private void Update()
        {
             _soulMovementController.UpdateMovement(_movementStyle); 
             MaybeFlipSprite();
        }
        
        private void MaybeFlipSprite()
        {
            if (_soulMovementController.MovementDirection.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
            else if (_soulMovementController.MovementDirection.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }


        // collection 
        public void OnCollected()
        {
            gameManagerChannel.AddScore(gameState.CurrentSoulValue);
            Destroy(gameObject);
        }

        public MovementStyle GetRandomMovementStyle()
        {
            var values = (MovementStyle[])System.Enum.GetValues(typeof(MovementStyle));
            return values[Random.Range(0, values.Length)];
        }
    }
}
