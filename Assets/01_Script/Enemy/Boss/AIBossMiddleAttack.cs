using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBossMiddleAttack : MonoBehaviour
{
    [SerializeField] int Damaged = 1000;
    AIState AI;
    Vector3 vec;
    [SerializeField]Collider[] hit;
    [SerializeField] LayerMask layer;
    float Delay = 0;

    bool Started = false;

    private void Awake()
    {
        AI = GetComponentInParent<AIState>();
    }

    private void Update()
    {
        Delay -= Time.deltaTime;
    }
    public void MiddleAttack()
    {
        AI.Doing = true;
        vec = (PlayerManager.Instance.PlayerS.position - transform.position).normalized;
        vec.y = 0;
        vec.z = 0;
        Delay = 2;
        if(Started == false)
            AI.main._rigid.AddForce(vec * 20, ForceMode.Impulse);

        Started = true;
        if ((PlayerManager.Instance.Player.position - transform.position).normalized.x < 0)
        {
            if(AI.main.transform.localScale.x > 0)
                AI.main.transform.localScale = new Vector3(-AI.main.transform.localScale.x, 3, 0);
        }
        else
        {
            if (AI.main.transform.localScale.x < 0)
                AI.main.transform.localScale = new Vector3(-AI.main.transform.localScale.x, 3, 0);
        }

        AI.main.Ani.SetTrigger("SPAttack");
        AI.main.Audio.PlayOneShot(AI.main.cllip);
        StartCoroutine(End());
    }
    void OnDrawGizmos()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(-0.1f, 0, 0), new Vector3(1f, 0.3f, 1));
    }

    IEnumerator End()
    {
        yield return null;
        hit = Physics.OverlapBox(transform.position + new Vector3(-0.1f,0,0), new Vector3(1f,0.3f,1), Quaternion.identity, layer);
        AI.Cool2 = AI.main._info.skills[1].CoolTime;

        if (hit.Length != 0)
        {
            Debug.Log(hit.Length);
            Started = false;
            hit[0].GetComponent<PlayerStat>().Damaged(Random.Range(Damaged-200, Damaged+201));
            hit[0].transform.position += new Vector3(0, 0.3f, 0);
            PlayerManager.Instance.CanControl = false;
            yield return null;
            hit[0].GetComponent<Rigidbody>().velocity = new Vector3(vec.x * 5, 4f, 0);
            AI.Doing = false;
            AI.main._rigid.velocity = Vector3.zero;
            
        }
        else if (Delay > 0)
        {
            StartCoroutine(End());
        }
        else
        {
            Started = false;
            AI.Doing = false;
            AI.main._rigid.velocity = Vector3.zero;
        }
    }
}
