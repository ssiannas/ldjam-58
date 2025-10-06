using System;
using UnityEngine;

namespace ldjam_58
{
    public class WeaponSwooshController : MonoBehaviour
    {
       private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                throw new MissingComponentException("Animator component is missing from " +
                                                    "WeaponSwooshController GameObject");
            }
        }

        private void Start()
        {
            var animationDuration = _animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationDuration);
        }
    }
}
