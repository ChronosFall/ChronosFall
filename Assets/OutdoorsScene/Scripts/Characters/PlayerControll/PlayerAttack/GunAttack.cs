using UnityEngine;
using GameAnimations;

public class GunAttack : MonoBehaviour
{
    [Header("Animator")] public Animator animator;
    [Header("GunType(Int)")] public int gunType;

    void Start()
    {
        //アニメーターを取得
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        // 銃の種類の切り替え(Debug)
        animator.SetInteger(PlayerAttackAnimator.WeaponType, gunType);

        // Aimingアニメーション
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool(PlayerAttackAnimator.IsAiming, true);
        }
        else
        {
            animator.SetBool(PlayerAttackAnimator.IsAiming, false);
        }
        
        // Reloadアニメーション
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger(PlayerAttackAnimator.ReloadTrigger);
        }
        
        // Shootアニメーション
        if (Input.GetMouseButtonDown(0) && animator.GetBool(PlayerAttackAnimator.IsAiming))
        {
            animator.SetTrigger(PlayerAttackAnimator.ShootTrigger);
        }
    }
}