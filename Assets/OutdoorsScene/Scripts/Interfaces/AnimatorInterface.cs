using UnityEngine;


namespace GameAnimations
{
    /// <summary>
    /// プレイヤーが移動する場合のアニメーション
    /// </summary>
    public static class PlayerMoveAnimator
    {
        public static readonly int IsWalking = Animator.StringToHash("IsWalking");
        public static readonly int SpeedAxisX = Animator.StringToHash("Speed_AxisX");
        public static readonly int SpeedAxisY = Animator.StringToHash("Speed_AxisY");
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
}