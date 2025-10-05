using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ldjam_58
{
    public class MinionController : MonoBehaviour
    {
        [SerializeField] private float reapPeriod = 2.0f;
        [SerializeField] private float reapRadius = 1.0f;
        [SerializeField] private GameManagerChannel _gameManagerChannel;
        
        private readonly Collider2D[] _soulsBuffer = new Collider2D[600];
        private Vector2 _reapPoint;
        private ContactFilter2D _contactFilter;
        private LayerMask _soulLayerMask;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            // reap downwards from the minion position
            _soulLayerMask = 1 << LayerMask.NameToLayer("Souls");
            _reapPoint = transform.position - new Vector3(0, reapRadius / 2);
            _contactFilter = ContactFilter2D.noFilter;
            _contactFilter.SetLayerMask(_soulLayerMask);
        }

        IEnumerator StartReap()
        {
            yield return new WaitForFixedUpdate();
            while (true)
            {
                DoReap();
                yield return new WaitForSeconds(reapPeriod);
            }
        }

        private void DoReap()
        {
            // Play Aimation
            var numHits = Physics2D.OverlapCircle(_reapPoint, reapRadius, _contactFilter, _soulsBuffer);
            Debug.Log($"Minion reaped {numHits} souls");
            for (var i = 0; i < numHits; i++)
            {
                var soul = _soulsBuffer[i].GetComponent<SoulController>();
                soul?.OnCollected();
            }
        }
        
        public void EnableMinion()
        {
            // Maybe play an animation in the future and then enable the minion in a state
            gameObject.SetActive(true);
            StartCoroutine(StartReap());
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_reapPoint, reapRadius);
        }
    }
}
