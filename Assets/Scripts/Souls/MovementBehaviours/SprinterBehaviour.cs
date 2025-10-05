using UnityEngine;

namespace ldjam_58
{
    public class SprinterBehaviour : MovementBehaviourBase
    {
        private float sprintDuration = 0.35f;
        private float sprintInterval = 1.5f; // Total cycle time (sprint + rest)
        private float sprintSpeedMultiplier = 2.0f;
        
        private bool _sprinting;
        private float _nextSprintToggle;
        public override void OnEnter(MovementContext context)
        {
            base.OnEnter(context);
            _sprinting = false;
            _nextSprintToggle = 0f;

            // Set running animation
        }

        public override void Update(MovementContext context, float time)
        {
            switch (_sprinting)
            {
                case false when time >= _nextSprintToggle:
                    DoSprint(context, time);
                    break;
                case true when time >= _nextSprintToggle:
                    StopSprint(context, time);
                    break;
            }
        }
        
        private void DoSprint(MovementContext context, float time)
        {
            _sprinting = true;
            _nextSprintToggle = time + sprintDuration;
        }

        private void StopSprint(MovementContext context, float time)
        {
            _sprinting = false;
            _nextSprintToggle = time + (sprintInterval - sprintDuration);
        }
        public override void OnExit(MovementContext context)
        {
            // Reset running animation 
            base.OnExit(context);
        }
        
        public override float GetSpeedMultiplier()
        {
            return _sprinting ? sprintSpeedMultiplier : 1.0f;
        }
    }
}