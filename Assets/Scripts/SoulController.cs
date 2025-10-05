using Unity.VisualScripting;
using UnityEngine;

namespace ldjam_58
{
    public class SoulController : MonoBehaviour
    {
        public enum MovementStyle
        {
            Runner,
            Sprinter,
            Hopper,
            SineWave,
            ZigZag,
            Ducker
        }

        [Header("Soul Settings")] public MovementStyle movementStyle = MovementStyle.Runner;
        public float moveSpeed = 5f;
        public int soulPoints = 1;

        [Header("Sprinter")] public float sprintMultiplier = 2f;
        public float sprintDuration = 0.35f;
        public float sprintInterval = 1.5f;

        [Header("Hopper (add Rigidbody2D for physics)")]
        public float hopInterval = 0.8f;

        public float hopForce = 6f; // only for Rigidbody2D exists
        public float hopHeightNoRb = 0.75f; // fallback for no Rigidbody2D

        [Header("Sine Wave / ZigZag")] public float verticalAmplitude = 0.75f;
        public float verticalFrequency = 2f;

        [Header("Ducker")] [Range(0f, 0.8f)] public float duckAmount = 0.4f; // Y - scale reduction
        public float duckDuration = 0.25f;
        public float duckInterval = 1.2f;

        private Vector3 _startPosition;
        private Vector3 _originalScale;
        private Rigidbody2D _rb;

        // timers and state
        private float _t;
        private bool _sprinting;
        private float _nextSprintToggle;
        private float _nextHopTime;
        private float _hopStartY;
        private bool _ducking;
        private float _nextDuckTime;

        // duck doooooooooooodgers

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _startPosition = transform.position;
            _originalScale = transform.localScale;

            _t = 0f;
            _nextSprintToggle = sprintInterval;
            _nextHopTime = hopInterval;
            _nextDuckTime = duckInterval;  // how long until first duck event
            _hopStartY = _startPosition.y; // store starting height for hop motion

        }

        private void Update()
        {
            _t += Time.deltaTime;

            // base horizontal movement
            float speed = moveSpeed * ((movementStyle == MovementStyle.Sprinter && _sprinting) ? sprintMultiplier : 1f);
            transform.position += Vector3.right * (speed * Time.deltaTime);

            switch (movementStyle)
            {
                case MovementStyle.Runner: break;
                case MovementStyle.Sprinter: UpdateSprinter(); break;
                case MovementStyle.Hopper: UpdateHopper(); break;
                case MovementStyle.SineWave: UpdateSine(); break;
                case MovementStyle.ZigZag: UpdateZigZag(); break;
                case MovementStyle.Ducker: UpdateDucker(); break;
            }
        }

        // ta mood swings tous
        void UpdateSprinter()
        {
            if (!_sprinting && _t >= _nextSprintToggle)
            {
                _sprinting = true;
                _nextSprintToggle = _t + sprintDuration;
            }
            else if (_sprinting && _t >= _nextSprintToggle)
            {
                _sprinting = false;
                _nextSprintToggle = _t + (sprintInterval - sprintDuration);
            }
        }

        void UpdateHopper()
        {
            if (_rb != null)
            {
                if (_t >= _nextHopTime)
                {
                    _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
                    _rb.AddForce(Vector2.up * hopForce, ForceMode2D.Impulse);
                    _nextHopTime = _t + hopInterval;
                }
            }
            else
            {
                if (_t >= _nextHopTime)
                {
                    _hopStartY = transform.position.y;
                    _nextHopTime = _t + hopInterval;
                }
                float phase = Mathf.PingPong(_t * (Mathf.PI / hopInterval), Mathf.PI);
                float y = _hopStartY + Mathf.Sin(phase) * hopHeightNoRb;
                var p = transform.position;
                p.y = y; transform.position = p;
            }
        }

        void UpdateSine()
        {
            float y = _startPosition.y + Mathf.Sin(2f * Mathf.PI * verticalFrequency * _t) * verticalAmplitude;
            var p = transform.position;
            p.y = y; transform.position = p;
        }

        void UpdateZigZag()
        {
            float tri = Mathf.PingPong(_t * verticalFrequency, 1f) * 2f - 1f; // apo -1..1
            float y = _startPosition.y + tri * verticalAmplitude;
            var p = transform.position;
            p.y = y; transform.position = p;
        }


        void UpdateDucker()
        {
            // enter duck
            if (!_ducking && _t >= _nextDuckTime)
            {
                _ducking = true;
                _nextDuckTime = _t + duckDuration;
                transform.localScale = new Vector3(
                    _originalScale.x,
                    _originalScale.y * (1f - duckAmount),
                    _originalScale.z);
            }
            // exit duck
            else if (_ducking && _t >= _nextDuckTime)
            {
                _ducking = false;
                _nextDuckTime = _t + (duckInterval - duckDuration);
                transform.localScale = _originalScale;
            }
        }

        // collection 
        public void OnCollected() => Destroy(gameObject);



        public MovementStyle GetRandomMovementStyle()
        {
            var values = (MovementStyle[])System.Enum.GetValues(typeof(MovementStyle));
            return values[Random.Range(0, values.Length)];
        }
    }
}
