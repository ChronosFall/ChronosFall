using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using NUnit.Framework;

public class Gun_Attack : MonoBehaviour
{
    [Header("Animator")] public Animator animator;
    [Header("GunType(Int)")] public int GunType = 0;


    void Start()
    {
        //アニメーターを取得
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // 銃の種類の切り替え(Debug)
        animator.SetInteger("WeaponType", GunType);

        // Aimingアニメーション
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("IsAiming", true);
        }
        else
        {
            animator.SetBool("IsAiming", false);
        }
        // Reloadアニメーション
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("ReloadTrigger");
        }
        // Shootアニメーション
        if (Input.GetMouseButtonDown(0) && animator.GetBool("IsAiming"))
        {
            animator.SetTrigger("ShootTrigger");
        }
    }
}
