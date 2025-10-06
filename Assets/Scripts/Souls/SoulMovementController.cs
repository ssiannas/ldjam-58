using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace ldjam_58
{
    public class SoulMovementController : MonoBehaviour
    {
        public enum SoulMovementStyle
        {
            Runner,    
            Sprinter,  
            Hopper,    
            SineWave,  
            ZigZag,    
        }
        
        public float moveSpeed = 5f;
        public float _t;
        
        
        private Rigidbody2D _rb;
        
        private Animator _animator;
        private IMovementBehavior _currentBehavior;
        private Dictionary<SoulMovementStyle, IMovementBehavior> _behaviours;
        private IMovementBehavior _defaultBehavior;
        private Camera _mainCamera;

        public Vector2 MovementDirection
        {
            get => _direction;
            private set => _direction = value;
        }
        private Vector2 _direction;

        
        private void InitializeBehaviors()
        {
            _behaviours = new Dictionary<SoulMovementStyle, IMovementBehavior>
            {
                { SoulMovementStyle.Runner, new RunnerBehaviour() },
                { SoulMovementStyle.Sprinter, new SprinterBehaviour() },
                { SoulMovementStyle.Hopper, new HopperBehaviour() },
                { SoulMovementStyle.SineWave, new SineWaveBehaviour() },
                { SoulMovementStyle.ZigZag, new ZigZagBehaviour() },
            };
        }

        private MovementContext CreateContext()
        {
            return new MovementContext(this, _animator, _rb);
        }
        
        public void SetMovementStyle(SoulMovementStyle style)
        {
            if (_behaviours.TryGetValue(style, out var behavior))
            {
                var context = CreateContext();
                _currentBehavior ??= _defaultBehavior;
                _currentBehavior.OnExit(context);
                _currentBehavior = behavior;
                _currentBehavior.OnEnter(context);
            }
        }

        private void SetInitialDirection()
        {
            _direction = new Vector2(DirectionToCenter().x, 0);
        }
        
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _defaultBehavior = new RunnerBehaviour();
            _mainCamera = Camera.main;
           InitializeBehaviors(); 
           SetInitialDirection();
        }
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private Vector3 DirectionToCenter()
        {
            var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Vector2 worldCenter = _mainCamera.ScreenToWorldPoint(screenCenter);
            var direction = worldCenter - (Vector2)transform.position;
            return direction.normalized;
        }

        public void UpdateMovement(SoulMovementStyle movementStyle)
        {
            _t += Time.deltaTime;

            if (_behaviours[movementStyle] != _currentBehavior)
            {
                SetMovementStyle(movementStyle);
            }

            _currentBehavior.Update(CreateContext(), _t);

            var speed = moveSpeed * _currentBehavior.GetSpeedMultiplier();
            transform.position += (Vector3)_direction * (speed * Time.deltaTime);
        }
    }
}
