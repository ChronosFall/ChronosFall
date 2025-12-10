using UnityEngine;

namespace ChronosFall.Scripts.Interfaces
{
    /// <summary>
    /// プレイヤーが移動する場合のアニメーション
    /// </summary>
    public static class PlayerMovementAnimator
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int SpeedAxisX = Animator.StringToHash("SpeedAxisX");
        public static readonly int SpeedAxisY = Animator.StringToHash("SpeedAxisY");
    }
    /// <summary>
    /// プレイヤーが攻撃する場合のアニメーション
    /// </summary>
    public static class PlayerAttackAnimator
    {
        public static readonly int IsAiming = Animator.StringToHash("IsAiming");
        public static readonly int WeaponType = Animator.StringToHash("WeaponType");
        public static readonly int ReloadTrigger = Animator.StringToHash("ReloadTrigger");
        public static readonly int ShootTrigger = Animator.StringToHash("ShootTrigger");
    }

    /// <summary>
    /// プレイヤーのその他アニメーション
    /// </summary>
    public static class PlayerOtherAnimator
    {
        public static readonly int IsDead = Animator.StringToHash("IsDead");
    }

    /// <summary>
    /// 敵のアニメーション
    /// </summary>
    public static class DefaultEnemyAnimator
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int IsAttacking =  Animator.StringToHash("IsAttacking");
    }
}