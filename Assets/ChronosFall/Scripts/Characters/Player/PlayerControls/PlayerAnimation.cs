using ChronosFall.Scripts.Interfaces;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private bool tempStopAnimator;

        private Animator _animator;
        private Rigidbody _rb;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            UpdateAnimation();
        }

        /// <summary>
        /// アニメーション更新
        /// </summary>
        private void UpdateAnimation()
        {
            if (!_animator || !_animator.runtimeAnimatorController || tempStopAnimator) return;
            
            _animator.speed = 1.0f;
            _animator.SetFloat(PlayerMovementAnimator.MoveSpeed, _rb.linearVelocity.magnitude);
        }
    }
}