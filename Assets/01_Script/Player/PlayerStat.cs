using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static bool Barrier = false;


    [Header("정보")]
    [SerializeField] float HP = 10000;
    [SerializeField] float ATK = 1;
    [SerializeField] Image hpbar;
    private void Update()
    {
        hpbar.fillAmount = HP / 10000f;
    }

    public void Damaged(int Damaged)
    {
        if (GetComponent<PlayerMove>().DoDoged == false)
            return;
        if (Input.GetKey(KeyCode.F) && GetComponent<PlayerAttack>().CurrentGuns > Damaged / 200 && Barrier == false)
        {
            GetComponent<PlayerAttack>().CurrentGuns = GetComponent<PlayerAttack>().CurrentGuns - Damaged / 200;
            StartCoroutine(BarrierCo());
            Barrier = true;
            Debug.Log("방어 성공 이펙트");

            return;
        }
        else
        {
            Barrier = false;

        }

        HP -= Damaged;
    }

    IEnumerator BarrierCo()
    {
        yield return null; yield return null;
        PlayerManager.Instance.CanControl = true;
        Barrier = false;
    }
}
