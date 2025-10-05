namespace ldjam_58
{
    public class ZigZagBehaviour : MovementBehaviourBase
    {

        private readonly float _zigZagAmplitude = 3f;
        private readonly float _zigZagFrequency = 0.5f;

        private float _startY;
        private int _currentDirection = 1; // 1 for up, -1 for down

        public override void OnEnter(MovementContext context)
        {
            base.OnEnter(context);
            _startY = context.Transform.position.y;
            _currentDirection = 1;
        }

        public override void Update(MovementContext context, float time)
        {
            // Triangle wave for zig-zag pattern
            var normalizedTime = (time * _zigZagFrequency) % 1f;
            var triangleWave = normalizedTime < 0.5f ? normalizedTime * 2f : 2f - (normalizedTime * 2f);

            var y = _startY + (triangleWave - 0.5f) * 2f * _zigZagAmplitude;

            var position = context.Transform.position;
            position.y = y;
            context.Transform.position = position;

            // Determine direction for animation
            var newDirection = triangleWave < 0.5f ? 1 : -1;
            _currentDirection = newDirection;

        }

        public override void OnExit(MovementContext context)
        {
            base.OnExit(context);
        }
    }
}
