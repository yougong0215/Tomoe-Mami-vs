using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossNormalAttack : MonoBehaviour
{
    AIState AI;
    Vector3 vec;
    private void Awake()
    {
        AI = GetComponentInParent<AIState>();
    }

    [SerializeField] Collider[] hit;
    [SerializeField] LayerMask layer;
    int dir;

    public void Attack()
    {
        AI.Doing = true;
        if (AI.main.transform.localScale.x > 0)
            dir = 1;
        else if (AI.main.transform.localScale.x < 0)
            dir = -1;
        AI.main.Ani.SetTrigger("Attack");
        AI.main.Audio.PlayOneShot(AI.main.cllip);
        hit = Physics.OverlapBox(transform.position + new Vector3(-0.1f, 0, 0) + new Vector3(dir,0,0), new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, layer);
        if(hit.Length > 0)
        {
            hit[0].gameObject.GetComponent<PlayerStat>().Damaged(800);
            StartCoroutine(i());
        }
        else
        {
            AI.Doing = false;

        }
        AI.Cool3 = AI.main._info.skills[2].CoolTime;
    }
    IEnumerator i()
    {
        vec = (PlayerManager.Instance.PlayerS.position - transform.position).normalized;
        vec.y = 0;
        vec.z = 0;
        PlayerManager.Instance.CanControl = false;
        yield return null;
        if(AI.main._rigid.velocity.y > 1)
        {
            AI.main._rigid.AddForce(new Vector3(0, -10, 0), ForceMode.Impulse);
            AI.Cool1  =0.5f;
        }
        hit[0].GetComponent<Rigidbody>().velocity = new Vector3(vec.x * 3, 0) + new Vector3(0, -20, 0);

        yield return new WaitForSeconds(0.3f);
        PlayerManager.Instance.CanControl = true;
        AI.Doing = false;
    }
}
