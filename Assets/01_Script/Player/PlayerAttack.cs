using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{

    public static bool shot = false;
    [SerializeField] Transform MoveGun;
    [SerializeField] Transform GunFlat;
    [SerializeField] Transform GunsShoot;
    [SerializeField] int MaxGunHave;
    [SerializeField] int _currentGunHave;
    [SerializeField] TextMeshProUGUI GunHave;

    Coroutine Cool;
    Coroutine Flat;
    int _jumpPlatCount = 3;

    private void OnEnable()
    {
        Cool = StartCoroutine(Reload());
        Flat = StartCoroutine(SummonFlat());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && Mathf.Abs(GetComponent<PlayerMove>().MoveG) > 0.3f)
        {
            if(MaxGunHave >= _currentGunHave && _currentGunHave != 0)
            {

                PoolAble p = PoolManager.Instance.Pop(MoveGun.name);
                p.transform.position = PlayerManager.Instance.Player.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.3f, 0.9f), 0);
                _currentGunHave--;

                if(Cool !=null)
                {
                    StopCoroutine(Cool);
                }
                Cool = StartCoroutine(Reload());

            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && MaxGunHave >= _currentGunHave && _currentGunHave > 12)
        {
            if (MaxGunHave >= _currentGunHave && _currentGunHave != 0)
            {
                
                StartCoroutine(SHoot());
            }
        }
        if(MaxGunHave < _currentGunHave)
        {
            _currentGunHave = MaxGunHave;
        }
        else if(_currentGunHave < 0)
        {
            _currentGunHave = 0;
        }

        if(_jumpPlatCount < 0 )
        {
            _jumpPlatCount = 0;
        }


        GunHave.text = $"[ {_currentGunHave} / {MaxGunHave} ] ";
        //Debug.Log($"[ {_currentGunHave} / {MaxGunHave} ] ");
    }

    IEnumerator SHoot()
    {
        shot = true;
        int t = (int)(_currentGunHave * _currentGunHave * 0.2f);
        while(t != 0)
        {
            _currentGunHave = 0;
            yield return null;
            t--;
            PoolAble p = PoolManager.Instance.Pop(GunsShoot.name);
            p.transform.position = PlayerManager.Instance.Player.position + new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(0.3f, 0.9f), 0);
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
        p.transform.position = PlayerManager.Instance.Player.position + new Vector3(GetComponent<PlayerMove>().MoveG+ 0.35f, -0.5f, 0);
        p.gameObject.GetComponent<SpriteRenderer>().flipX = PlayerManager.Instance.Spi.flipX;
        _currentGunHave--;

    }

    IEnumerator Reload()
    {
        while(true)
        {
            Debug.Log(_currentGunHave * MaxGunHave * 0.0015f + 0.6f);
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
            if(_jumpPlatCount < 3)
                _jumpPlatCount++;

        }
    }
}