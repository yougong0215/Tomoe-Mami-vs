using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AIMain : MonoBehaviour
{
    [SerializeField] EnemyScriptable _info;

    [SerializeField] UnityEvent DieEvent = null;

    protected virtual void LateUpdate()
    {
       
    }

    protected virtual void DeadEvent()
    {
        if (_info.HP < 0 && _info.pase == 1)
        {
            DieEvent?.Invoke();
        }
    }

    protected virtual void Damaged()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAttack"))
        {

        }
    }
}
