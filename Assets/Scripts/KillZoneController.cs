using System;
using UnityEngine;

namespace ldjam_58
{
    public class KillZoneController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Check if the colliding object is in the "Souls" layer
            if (other.gameObject.CompareTag($"Soul"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
