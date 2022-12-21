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
            if (Random.Range(0f, 10f) > 8)
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

        vec = (PlayerManager.Instance.PlayerS.position - transform.position).normalized;
        vec.y = 0;
        vec.z = 0;
        AI.main._rigid.velocity = Vector3.zero;

        AI.main.Damage = 0;
        AI.main._rigid.AddForce(vec * 4, ForceMode.Impulse);
        AI.main.Ani.SetTrigger("Hurt");


        TextDamage tx = PoolManager.Instance.Pop("TextDamage") as TextDamage;

        tx.transform.position = AI.main.transform.position + new Vector3(0,0,-1);
        tx.Seting("Miss");


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


        yield return null;
    }

    IEnumerator Doing()
    {

        AI.main._info.HP += AI.main.Damage;
        if(AI.main.Damage < 0)
        {
            TextDamage tx = PoolManager.Instance.Pop("TextDamage") as TextDamage;
            tx.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.6f, 1.4f), -1);
            tx.Seting($"{-AI.main.Damage}");
        }
            AI.main.bDamaged = false;
            AI.main.Damage = 0;
        CameraManager.Instance.Noise(0.7f);

        yield return new WaitForSeconds(0.02f);


    }

}
