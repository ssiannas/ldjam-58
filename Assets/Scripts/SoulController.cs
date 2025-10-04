using UnityEngine;
using UnityEngine.InputSystem;

namespace ldjam_58
{
    public class SoulController : MonoBehaviour
    {
        [Header("Soul Settings")] public float moveSpeed = 5f; // Horizontal speed
        public int soulPoints = 1; // Points on Collection
        private Vector3 startPosition; //startPosition

        private void Start()
        {
            startPosition = transform.position;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ClickAndDestroy();
            }
        }

        void Update()
        {
            float newX = transform.position.x + (moveSpeed * Time.deltaTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }

    }

    void ClickAndDestroy()
    {
        Debug.Log("NPC destroyed!");
        Destroy(gameObject);
        Debug.Log("NPC destroyed!");
    }
        private void ClickAndDestroy()
        {

        }
    }
}