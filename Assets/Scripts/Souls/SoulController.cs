using Unity.VisualScripting;
using UnityEngine;

namespace ldjam_58
{
        
    using MovementStyle = SoulMovementController.SoulMovementStyle; 
    [RequireComponent(typeof(SoulMovementController))]
    public class SoulController : MonoBehaviour
    {
        
        [SerializeField] private GameState gameState;
        [SerializeField] private GameManagerChannel gameManagerChannel;
        
        private MovementStyle _movementStyle = MovementStyle.Runner;
        private SoulMovementController _soulMovementController;
        // duck doooooooooooodgers

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
        }


        private void Update()
        {
             _soulMovementController.UpdateMovement(_movementStyle); 
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
