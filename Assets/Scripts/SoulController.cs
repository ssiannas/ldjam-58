using UnityEngine;
using UnityEngine.InputSystem;

namespace ldjam_58
{
    public class SoulController : MonoBehaviour
    {
        [Header("Soul Settings")] public float moveSpeed = 5f; // Horizontal speed
        public int soulPoints = 1; // Points on Collection
        private Vector3 startPosition; //startPosition
        private int _direction = 1;
        
        private void Start()
        {
            startPosition = transform.position;
            if (startPosition.x > 0)
            {
                _direction = -1;
            }
        }

        void Update()
        {
            float newX = transform.position.x + (moveSpeed * Time.deltaTime * _direction);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

        public void OnColelcted()
        {
            // Trigger death animation
            // Trigger sound effect
            Destroy(gameObject);
        }
    }
}