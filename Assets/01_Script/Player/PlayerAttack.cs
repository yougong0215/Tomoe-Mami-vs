using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Transactions;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public static bool shot = false;
    public static bool attack = false;
    [SerializeField] Transform MoveGun;
    [SerializeField] Transform GunFlat;
    [SerializeField] Transform GunsShoot;
    [SerializeField] int MaxGunHave = 24;
    [SerializeField] int _currentGunHave = 0;
    [SerializeField] TextMeshProUGUI GunHave;
    [SerializeField] Image Guns;

    [SerializeField] LayerMask layer;
    [SerializeField] Image CoolTimeGuns;
    [SerializeField] Collider[] hit;
    [SerializeField] AudioSource audios;
    [SerializeField] AudioClip a;

    Coroutine Cool;
    Coroutine Attack;
    int _jumpPlatCount = 1;

    float ShootCooltime = 0;

    private void OnEnable()
    {
        Cool = StartCoroutine(Reload());
        StartCoroutine(SummonFlat());
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.Instance.CanControl)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (MaxGunHave >= _currentGunHave && _currentGunHave != 0)
                {

                    PoolAble p = PoolManager.Instance.Pop(MoveGun.name);
                    p.transform.position = PlayerManager.Instance.PlayerS.position + new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(0.3f, 1.2f), 0);
                    _currentGunHave--;

                    if (Cool != null)
                    {
                        StopCoroutine(Cool);
                    }
                    Cool = StartCoroutine(Reload());

                }
            }
            if (Input.GetKeyDown(KeyCode.Q) && MaxGunHave >= _currentGunHave && _currentGunHave >= 5 && ShootCooltime < 0)
            {
                if (MaxGunHave >= _currentGunHave && _currentGunHave != 0)
                {

                    StartCoroutine(SHoot());
                }
            }


            if (Input.GetMouseButtonDown(0) && attack == false)
            {
                if (Attack != null)
                {
                    StopCoroutine(AttackWid());
                }
                Attack = StartCoroutine(AttackWid());
            }

            if (MaxGunHave < _currentGunHave)
            {
                _currentGunHave = MaxGunHave;
            }
            else if (_currentGunHave < 0)
            {
                _currentGunHave = 0;
            }

            if (_jumpPlatCount < 0)
            {
                _jumpPlatCount = 0;
            }
        }
        ShootCooltime -= Time.deltaTime;
        CoolTimeGuns.fillAmount = ShootCooltime / 4f;

        GunHave.text = $"[ {_currentGunHave} / {MaxGunHave} ] ";
        //Debug.Log(_currentGunHave / MaxGunHave);
        Guns.fillAmount = (float)_currentGunHave / (float)MaxGunHave;
        //Debug.Log($"[ {_currentGunHave} / {MaxGunHave} ] ");

    }

    IEnumerator AttackWid()
    {
        attack = true;

        PlayerManager.Instance.Ani.SetBool("Attacking", true);

        if (PlayerManager.Instance.Ani.GetInteger("Attack") >= 2)
        {
            PlayerManager.Instance.Ani.SetInteger("Attack", 0);
        }
        if (transform.localScale.x < 0)
        {
            hit = Physics.OverlapBox(transform.position + new Vector3(-1.2f,1.4f,0), new Vector3(1f, 1f, 1f), Quaternion.identity, layer);
        }
        else
        {
            hit = Physics.OverlapBox(transform.position + new Vector3(1.2f, 1.4f, 0), new Vector3(1f, 1f, 1f), Quaternion.identity, layer);
        }
        audios.PlayOneShot(a);

        if(hit.Length > 0)
        {
            if (hit[0].GetComponent<AIMain>())
                hit[0].GetComponent<AIMain>().Damaged(Random.Range(200, 501));
            else if (hit[0].GetComponent<EnemyWing>())
            {
                hit[0].GetComponent<EnemyWing>().Push();
                TextDamage tx = PoolManager.Instance.Pop("TextDamage") as TextDamage;
                tx.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.6f, 1.4f), -1);
                tx.Seting($"Slash");
            }
        }
        

        yield return null;
        PlayerManager.Instance.Ani.SetInteger("Attack", 1 + PlayerManager.Instance.Ani.GetInteger("Attack"));
        yield return new WaitForSeconds(0.4f);
        PlayerManager.Instance.Ani.SetBool("Attacking", false);
        yield return null;
        attack = false;
    }


    IEnumerator SHoot()
    {
        shot = true;
        int t = (int)(5 * 2f);
        CurrentGuns -= 5;
        while(t != 0)
        {
            ShootCooltime = 4;

            yield return new WaitForSeconds(0.05f);
            t--;
            PoolAble p = PoolManager.Instance.Pop(GunsShoot.name);
            p.transform.position = PlayerManager.Instance.PlayerS.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.3f, 0.9f), 0);
            if (Cool != null)
            {
                StopCoroutine(Cool);
            }
            Cool = StartCoroutine(Reload());
        }
        shot = false;
        GunHoldShoot.shoot = true;
      
    }

    public bool CanShoot()
    {
        if (MaxGunHave >= _currentGunHave && _currentGunHave != 0 && _jumpPlatCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlatformGun()
    {
        //if (Flat != null)
        //{
        //    StopCoroutine(Flat);
        //}
        //Flat = StartCoroutine(SummonFlat());
        _jumpPlatCount--;
        PoolAble p = PoolManager.Instance.Pop(GunFlat.name);
        p.transform.position = PlayerManager.Instance.PlayerS.position + new Vector3(GetComponent<PlayerMove>().MoveG+ 0.35f, -0.5f, 0);
        p.gameObject.GetComponent<SpriteRenderer>().flipX = PlayerManager.Instance.Spi.flipX;
        _currentGunHave--;

    }

    IEnumerator Reload()
    {
        while(true)
        {
            //Debug.Log(_currentGunHave * MaxGunHave * 0.0015f + 0.6f);
            yield return new WaitForSeconds(_currentGunHave * MaxGunHave * 0.0015f + 0.6f);
            if(MaxGunHave > _currentGunHave)
                _currentGunHave++;
            yield return null;
        }
    }
    IEnumerator SummonFlat()
    {
        while(true)
        {
            yield return new WaitForSeconds(2.6f);
            if(_jumpPlatCount < 1)
                _jumpPlatCount++;

        }
    }

    public int CurrentGuns
    {
        get => _currentGunHave;
        set => _currentGunHave = value;
    }
}
