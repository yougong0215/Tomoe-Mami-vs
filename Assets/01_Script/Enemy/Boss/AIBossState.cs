using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossState : AIState
{
    bool wait = false;

    float a = 5;

    public override void BehaviorTree()
    {
        a -= Time.deltaTime;
        if(a < 0)
        {
            Doing = false;
        }

        if (main.bDamaged == true)
        {
            Debug.Log($"BT : Damaged");
            Damaged?.Invoke();
            return; // 1프레임 지연 문제...
        }

        Debug.Log($"BT : {Doing}");
        if (Doing == false && wait == false && main.Died  == false)
        {
            a = 3;

            if (OutRangedAttack != null && Cool1 < 0)
            {
                Debug.Log($"BT : Skill1");
                OutRangedAttack?.Invoke();
                return;
            }

            if (MiddleRangedAttack != null && Cool2 < 0)
            {
                Debug.Log($"BT : Skill2");
                if (Mathf.Abs(PlayerManager.Instance.PlayerS.position.y) - Mathf.Abs(transform.position.y) > 0.5f)
                {
                    MiddleRangedAttack?.Invoke();
                    return;
                }
            }

            if (NormalAttack != null && Cool3 < 0)
            {
                if (Vector3.Distance(PlayerManager.Instance.PlayerS.position, transform.position) < 1.2f)
                {
                    Debug.Log($"BT : Skill3");
                    NormalAttack?.Invoke();
                    return;
                }

                Debug.Log($"BT : MoveWork");
                MoveWork?.Invoke();
            }

        }
        else
        {
            Debug.Log($"BT : Idle");
            main.Ani.SetBool("Move", false);
        }
    }

}
