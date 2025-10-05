namespace ldjam_58
{
        public interface IMovementBehavior
        {
            void OnEnter(MovementContext context);
            void Update(MovementContext context, float time);
            void OnExit(MovementContext context);
            float GetSpeedMultiplier();
        }
}