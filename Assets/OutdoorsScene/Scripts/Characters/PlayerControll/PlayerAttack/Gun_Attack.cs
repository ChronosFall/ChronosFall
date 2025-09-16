using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Gun_Attack : MonoBehaviour
{
    [Header("アニメーション")] public Animator animator;


    void Start()
    {
        //アニメーターを取得
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Attack");
            Debug.Log("Attacking!") ;
        }
    }
}
