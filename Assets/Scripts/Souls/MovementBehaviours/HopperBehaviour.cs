using UnityEngine;

namespace ldjam_58
{
    public class HopperBehaviour : MovementBehaviourBase
    {
        private float _nextHopTime;
        private float _hopStartY;

        // These should be configurable via ScriptableObject or similar
        private float hopForce = 5f;
        private float hopInterval = 1f;
        private float hopHeightNoRb = 2f;

        public override void OnEnter(MovementContext context)
        {
            base.OnEnter(context);
            _nextHopTime = 0f;
            _hopStartY = context.Transform.position.y;
        }

        public override void Update(MovementContext context, float time)
        {
            if (context.Rigidbody != null)
            {
                UpdateWithRigidbody(context, time);
            }
            else
            {
                UpdateWithoutRigidbody(context, time);
            }
        }

        private void UpdateWithRigidbody(MovementContext context, float time)
        {
            if (time >= _nextHopTime)
            {
                context.Rigidbody.linearVelocity = new Vector2(context.Rigidbody.linearVelocity.x, 0f);
                context.Rigidbody.AddForce(Vector2.up * hopForce, ForceMode2D.Impulse);
                _nextHopTime = time + hopInterval;

            }
        }

        private void UpdateWithoutRigidbody(MovementContext context, float time)
        {
            if (time >= _nextHopTime)
            {
                _hopStartY = context.Transform.position.y;
                _nextHopTime = time + hopInterval;
                // Animation?    
            }

            float phase = Mathf.PingPong(time * (Mathf.PI / hopInterval), Mathf.PI);
            float y = _hopStartY + Mathf.Sin(phase) * hopHeightNoRb;

            var position = context.Transform.position;
            position.y = y;
            context.Transform.position = position;
        }
    }
}