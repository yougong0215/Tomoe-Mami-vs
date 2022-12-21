using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIState : MonoBehaviour
{
    [Header("�ǰ�")]
    public UnityEvent Damaged;
    [Header("���Ÿ� ����")]
    public UnityEvent OutRangedAttack;
    [Header("��������")]
    public UnityEvent MiddleRangedAttack;
    [Header("�Ϲݰ���")]
    public UnityEvent NormalAttack;
    [Header("�̵�")]
    public UnityEvent MoveWork;

    public float Cool1 = 0;
    public float Cool2 = 0;
    public float Cool3 = 0;

    /// <summary>
    /// �ൿ�� = true
    /// �ƴҽ� - false
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
