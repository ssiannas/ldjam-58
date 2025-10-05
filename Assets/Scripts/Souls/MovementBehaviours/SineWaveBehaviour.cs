using UnityEngine;

namespace ldjam_58
{
    public class SineWaveBehaviour : MovementBehaviourBase
    {
        private float waveAmplitude = 2f;
        private float waveFrequency = 1f;

        private float _startY;

        public override void OnEnter(MovementContext context)
        {
            base.OnEnter(context);
            _startY = context.Transform.position.y; 
            // SetAnimationBool(context.Animator, ANIM_IS_WAVING, true);
        }

        public override void Update(MovementContext context, float time)
        {
            var y = _startY + Mathf.Sin(time * waveFrequency * Mathf.PI * 2f) * waveAmplitude;

            var position = context.Transform.position;
            position.y = y;
            context.Transform.position = position;

            // Normalized phase for animation blending (0 to 1)
            // float phase = (Mathf.Sin(time * waveFrequency * Mathf.PI * 2f) + 1f) * 0.5f;
            // Animator SetAnimationFloat(context.Animator, ANIM_WAVE_PHASE, phase);
        }

        public override void OnExit(MovementContext context)
        {
            // SetAnimationBool(context.Animator, ANIM_IS_WAVING, false);
            base.OnExit(context);
        }
    }
}