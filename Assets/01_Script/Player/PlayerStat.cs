using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static bool Barrier = false;


    [Header("Á¤º¸")]
    [SerializeField] float HP = 10000;
    [SerializeField] float ATK = 1;
    [SerializeField] Image hpbar;
    private void Update()
    {
        hpbar.fillAmount = HP / 10000f;
    }
    TextDamage tx;

    public void Damaged(int Damaged)
    {
        if (GetComponent<PlayerMove>().DoDoged == false)
        {
            tx = PoolManager.Instance.Pop("TextDamage") as TextDamage;

            tx.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.6f, 1.4f), -1);
            tx.Seting("Doged");
            return;
        }
        else if (Input.GetKey(KeyCode.F) && GetComponent<PlayerAttack>().CurrentGuns > Damaged / 200 && Barrier == false)
        {
            GetComponent<PlayerAttack>().CurrentGuns = GetComponent<PlayerAttack>().CurrentGuns - Damaged / 200;
            StartCoroutine(BarrierCo());
            Barrier = true;

            tx = PoolManager.Instance.Pop("TextDamage") as TextDamage;

            tx.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.6f, 1.4f), -1);
            tx.Seting("Guard");

            return;
        }
        else
        {
            StartCoroutine(T(Damaged));
        }
    }

    IEnumerator T(float Damaged)
    {
        Barrier = false;

        tx = PoolManager.Instance.Pop("TextDamage") as TextDamage;

        tx.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.6f, 1.4f), -1);
        tx.Seting($"{Damaged}");
        Time.timeScale = 0.1f;
        CameraManager.Instance.Noise(1f);
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 1;
        HP -= Damaged;
    }

    IEnumerator BarrierCo()
    {
        yield return null; yield return null;
        PlayerManager.Instance.CanControl = true;
        Barrier = false;
    }
}
