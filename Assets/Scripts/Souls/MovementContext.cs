using UnityEngine;

namespace ldjam_58
{
    public class MovementContext
    {
        public SoulMovementController MovementController { get; }
        public Animator Animator { get; }
        public Rigidbody2D Rigidbody { get; }
        public Transform Transform => MovementController.transform;

        public MovementContext(SoulMovementController movementController, Animator animator, Rigidbody2D rb)
        {
            MovementController = movementController;
            Animator = animator;
            Rigidbody = rb;
        }
    }
}