using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossOutRangeAttack : MonoBehaviour
{
    AIState AI;
    private void Awake()
    {
        AI = GetComponentInParent<AIState>();
    }

    float Slash = 50;
    float Order = 50;
    float t;


    public void OutRangeAttack()
    {
        AI.Doing = true;
        t = Random.Range(0f, 100f);

        if(Order <= Slash)
        {
            StartCoroutine(SlashCo("Slash", 0.1f, Random.Range(3, 7)));
        }
        else if(Slash <= Order)
        {
            StartCoroutine(SlashCo("Slash", 0.1f, Random.Range(3,9)));
        }

    }

    IEnumerator SlashCo(string tt, float sec, int again)
    {
        EnemyWing e = PoolManager.Instance.Pop(tt) as EnemyWing;
        Order += t;
        e.SetState(AI.main.transform.localScale.x, Random.Range(8.0f, 11.0f));
        e.transform.position = transform.position;
        AI.main.Ani.SetTrigger("SPAttack");
        if (Slash >= 100)
        {
            Slash = 50;
            Debug.Log("Skill");
        }
        AI.main.Audio.PlayOneShot(AI.main.cllip);

        yield return new WaitForSeconds(sec);

        if (again > 1)
        {
           
            StartCoroutine(SlashCo("Slash", Random.Range(0.1f, 0.5f), --again));
        }
        else
        {
            AI.Doing = false;
            AI.Cool1 = AI.main._info.skills[0].CoolTime;
        }
    }


}
