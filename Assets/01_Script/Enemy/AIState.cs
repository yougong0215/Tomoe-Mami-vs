using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIState : MonoBehaviour
{
    [Header("피격")]
    public UnityEvent Damaged;
    [Header("원거리 공격")]
    public UnityEvent OutRangedAttack;
    [Header("돌진공격")]
    public UnityEvent MiddleRangedAttack;
    [Header("일반공격")]
    public UnityEvent NormalAttack;
    [Header("이동")]
    public UnityEvent MoveWork;

    public float Cool1 = 0;
    public float Cool2 = 0;
    public float Cool3 = 0;

    /// <summary>
    /// 행동중 = true
    /// 아닐시 - false
    /// </summary>
    public bool Doing = false;

    public AIMain main;

    private void Awake()
    {
        main = GetComponentInParent<AIMain>();
    }

    private void Update()
    {
        if(Doing == false)
        {
            Cool1 -= Time.deltaTime;
            Cool2 -= Time.deltaTime;
            Cool3 -= Time.deltaTime;
        }
    }

    public virtual void BehaviorTree()
    {
        if(Doing == false)
        {
            if(main.bDamaged == true)
            {
                Damaged?.Invoke();
                return;
            }
        }
    }



}
