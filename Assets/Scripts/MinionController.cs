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

        private Animator _animator;
        private bool _isReaping = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameObject.SetActive(false);
            _animator = GetComponent<Animator>();
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
                StartCoroutine(DoReapDelay());
                yield return new WaitForSeconds(reapPeriod);
            }
        }


        private IEnumerator DoReapDelay()
        {
            _isReaping = true;
            yield return new WaitForSeconds(0.1f);

            _animator.SetTrigger("IsReaping");

            // Wait for specific frame in animation, then do damage
            yield return new WaitForSeconds(0.3f); // Time until impact frame

            DoReap(); // Deal damage at the right moment

            // Wait for rest of animation
            yield return new WaitForSeconds(0.5f);

            _isReaping = false;
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
