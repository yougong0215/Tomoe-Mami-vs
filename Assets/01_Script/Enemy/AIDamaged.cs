using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDamaged : MonoBehaviour
{
    AIState AI;

    Vector3 vec;

    private void Awake()
    {
        AI = GetComponentInParent<AIState>();
    }

    public void AIDamagedEvent()
    {
        if(Random.Range(0f,10f) > 8)
        {
            StartCoroutine(Woing());
        }
        else
        {
            StartCoroutine(Doing());
        }

    }

    IEnumerator Woing()
    {
        AI.main._info.Barrier--;
        AI.main.bDamaged = false;
        AI.Doing = true;

        vec = (PlayerManager.Instance.PlayerS.position - transform.position).normalized;
        vec.y = 0;
        vec.z = 0;
        AI.main._rigid.velocity = Vector3.zero;

        AI.main.Damage = 0;
        AI.main._rigid.AddForce(vec * 4, ForceMode.Impulse);
        AI.main.Ani.SetTrigger("Hurt");


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


        yield return new WaitForSeconds(0.4f);

        AI.Doing = false;
    }

    IEnumerator Doing()
    {
        AI.main._info.HP += AI.main.Damage;
        AI.main.Damage = 0;
        yield return new WaitForSeconds(0.1f);
        Debug.Log($"데미지 받기 : {AI.main.Damage}");

        
        AI.main.bDamaged = false;
    }

}
