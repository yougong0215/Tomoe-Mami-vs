using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossMoveWork : MonoBehaviour
{
    AIState AI;
    private void Awake()
    {
        AI = GetComponentInParent<AIState>();
    }

    public void Move()
    {
        float vec = Vector3.Distance(PlayerManager.Instance.Player.position, transform.position);
        if (vec > 1f)
        {
            if (PlayerManager.Instance.Player.position.y - transform.position.y > 1 && AI.main.CanJump == true)
            {
                AI.main.Ani.SetTrigger("Jump");
                AI.main._rigid.velocity = new Vector3(AI.main._rigid.velocity.x, 7, 0);
            }
            else if(AI.main._rigid.velocity.y > -3f)
            {
                AI.main._rigid.velocity -= new Vector3(0, 2, 0) * Time.deltaTime;
            }
            AI.main.Ani.SetBool("Move", true);
            AI.main._rigid.velocity = new Vector3((PlayerManager.Instance.Player.position - transform.position).normalized.x * 4, AI.main._rigid.velocity.y, 0);
            if ((PlayerManager.Instance.Player.position - transform.position).normalized.x < 0)
            {
                if (AI.main.transform.localScale.x > 0)
                    AI.main.transform.localScale = new Vector3(-AI.main.transform.localScale.x, 3, 0);
            }
            else 
            {

                if (AI.main.transform.localScale.x < 0)
                    AI.main.transform.localScale = new Vector3(-AI.main.transform.localScale.x, 3, 0);
            }

        }

    }
}
