using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWing : PoolAble
{
    float speed = 10;
    int dir;
    public void SetState(float f, float speed = 10f)
    {
        this.speed = speed;
        if (f > 0)
        {
            if (transform.localScale.x > 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            this.dir = -1;
        }
        else
        {
            if (transform.localScale.x < 0)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);

            this.dir = 1;
        }

       // Debug.Log(transform.localScale);

        transform.localScale = new Vector3(1, this.dir, 1);
    }


    private void Update()
    {
        transform.position += dir * transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStat>().Damaged(Random.Range(400,601));
        }
    }
}
