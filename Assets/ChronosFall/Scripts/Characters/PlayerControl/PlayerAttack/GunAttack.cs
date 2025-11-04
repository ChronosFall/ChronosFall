using ChronosFall.Scripts.Interfaces;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.PlayerControl.PlayerAttack
{
    public class GunAttack : MonoBehaviour
    {
        [Header("Animator")] private Animator _animator;

        [Header("[DEBUG] Gun type")] [SerializeField]
        private int gunType;

        private void Start()
        {
            _animator = Components.GetComponent<Animator>(gameObject);
        }
        private void Update()
        {
            // 銃の種類の切り替え(Debug)
            _animator.SetInteger(PlayerAttackAnimator.WeaponType, gunType);

            // Aimingアニメーション
            if (Input.GetMouseButtonDown(1))
            {
                _animator.SetBool(PlayerAttackAnimator.IsAiming, true);
            }
            else
            {
                _animator.SetBool(PlayerAttackAnimator.IsAiming, false);
            }

            // Reloadアニメーション
            if (Input.GetKeyDown(KeyCode.R))
            {
                _animator.SetTrigger(PlayerAttackAnimator.ReloadTrigger);
            }

            // Shootアニメーション
            if (Input.GetMouseButtonDown(0) && _animator.GetBool(PlayerAttackAnimator.IsAiming))
            {
                _animator.SetTrigger(PlayerAttackAnimator.ShootTrigger);
            }
        }
    }
}