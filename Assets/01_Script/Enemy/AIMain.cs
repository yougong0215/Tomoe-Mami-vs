using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EnemyAnimator
{
    Idle = 0,
    Damaged = 1
}

public class AIMain : MonoBehaviour
{
    [Header("정보")]
    [SerializeField] public EnemyScriptable _info;

    [Header("이벤트")]
    [SerializeField] UnityEvent DieEvent = null;
    [SerializeField] UnityEvent BT = null;

    [Header("에니메이터")]
    [SerializeField] public Animator Ani = null;
    [SerializeField] public List<string> AniName = new List<string>();

    [Header("물리")]
    [SerializeField] public Rigidbody _rigid;

    [Header("체력바")]
    [SerializeField] public Image _hpBar = null;

     public bool CanJump;
     public float Damage = 0;
    public bool Died = false;

    private void Awake()
    {
        Ani = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody>();
    }

    public bool bDamaged 
    {
        get;
        set;
    }

    private void Update()
    {
        BT?.Invoke();
        _hpBar.fillAmount = (float)_info.HP / (float)10000f;
        
    }



    protected virtual void LateUpdate()
    {
       if(_info.HP < 0)
       {
            DeadEvent();
       }
    }

    protected virtual void DeadEvent()
    {
        if (_info.HP < 0 && _info.pase == 1)
        {
            DieEvent?.Invoke();

            Ani.SetTrigger("Dead");
            Died = true;

            Debug.Log("주금");
        }
    }


    public void Damaged(float HP)
    {
        Damage -= HP;
        bDamaged = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Plat"))
        {
            CanJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        CanJump = false;
    }
}
