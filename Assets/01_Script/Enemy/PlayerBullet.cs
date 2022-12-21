using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : PoolAble
{
    bool die = false;


    private void Update()
    {

        if(die == false)
            transform.position += -transform.up * 20 * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Boss")
        {
            other.GetComponent<AIMain>().Damaged(Random.Range(200, 501));

            GetComponent<Animator>().SetTrigger("Die");
            StartCoroutine(Push());
        }

    }

    IEnumerator Push()
    {
        die = true;
        yield return new WaitForSeconds(0.3f);
        die = false;
        PoolManager.Instance.Push(this);
    }
}
