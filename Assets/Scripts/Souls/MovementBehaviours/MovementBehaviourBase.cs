using UnityEngine;

namespace ldjam_58
{
    public abstract class MovementBehaviourBase : IMovementBehavior
    {
        public virtual void OnEnter(MovementContext context)
        {
            // Override in derived classes
        }

        public abstract void Update(MovementContext context, float time);

        public virtual void OnExit(MovementContext context)
        {
            // Clean up animations
            ResetAnimations(context.Animator);
        }

        protected void ResetAnimations(Animator animator)
        {
            if (animator is null) return;
        }

        protected void SetAnimationTrigger(Animator animator, string triggerName)
        {
            animator?.SetTrigger(triggerName);
        }

        protected void SetAnimationBool(Animator animator, string paramName, bool value)
        {
            animator?.SetBool(paramName, value);
        }

        protected void SetAnimationFloat(Animator animator, string paramName, float value)
        {
            animator?.SetFloat(paramName, value);
        }

        public virtual float GetSpeedMultiplier() => 1.0f;
    }
}