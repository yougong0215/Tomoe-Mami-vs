using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveGround = 0;
    Rigidbody _rigid;
    bool canDoged = true;
    public bool DoDoged = true;
    bool OnGround = true;
    
    public float MoveG
    {
        get => _moveGround;
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, 0, 0));


        VelocityBind();

        if( PlayerAttack.attack == true || PlayerStat.Barrier == true)
        {
            _moveGround = 0;
            _rigid.velocity = new Vector3(0, -0.025f, 0);
            return;
        }
        if(PlayerManager.Instance.CanControl == false)
        {
            PlayerManager.Instance.Ani.SetBool("Hurt", true);

            return;
        }
        else
        {
            PlayerManager.Instance.Ani.SetBool("Hurt", false);

        }
        
        Jump();
        Doged();
        DoingDoged();


        Move();



        MoveAniBind();

    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(OnGround == true && DoDoged == true)
            {
                _rigid.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
                PlayerManager.Instance.Ani.SetBool("JumpEnd", false);
            }
            else // 만약에 총이 있으면
            {
                if(GetComponent<PlayerAttack>().CanShoot())
                {
                    _rigid.velocity = Vector3.zero;
                    GetComponent<PlayerAttack>().PlatformGun();
                }
                
            }
        }
    }

    private void VelocityBind()
    {
        if(_rigid.velocity.x > 6f)
        {
            _rigid.velocity += new Vector3(-30f, 0, 0) * Time.deltaTime;
        }
        else if (_rigid.velocity.x < -6)
        {
            _rigid.velocity += new Vector3(30f, 0, 0) * Time.deltaTime;
        }

        if(_rigid.velocity.y < -6 && PlayerManager.Instance.CanControl == false)
        {
            _rigid.velocity = new Vector3(_rigid.velocity.x, -6);
        }

    }

    private void DoingDoged()
    {
        if(DoDoged == false)
        {
            if (_moveGround >= -0.1f && _moveGround <= 0.1f)
            {
                if (transform.localScale == new Vector3(1, 1, 1)) // <<
                {
                    _rigid.velocity += new Vector3(-0.1f, 0, 0);
                }
                else // >>
                {
                    _rigid.velocity += new Vector3(0.1f, 0, 0);
                }
            }
            else
            {
                if (transform.localScale == new Vector3(1, 1, 1)) // <<
                {
                    _rigid.velocity += new Vector3(0.4f, 0, 0);
                }
                else // >>
                {
                    _rigid.velocity += new Vector3(-0.4f, 0, 0);
                }
            }
        }
    }

    private void Doged()
    {
        if(Input.GetKeyDown(KeyCode.E) && canDoged == true)
        {
            PlayerManager.Instance.Ani.SetBool("Doged", true);
            _rigid.velocity = Vector3.zero;
            StartCoroutine(IDoged());
        }
    }

    IEnumerator IDoged()
    {
        canDoged = false;
        DoDoged = false;

        _rigid.AddForce(new Vector3(0, 0.4f, 0), ForceMode.Impulse);

        yield return new WaitForSecondsRealtime(0.4f);
        DoDoged = true;
        PlayerManager.Instance.Ani.SetBool("Doged", false);
        yield return new WaitForSecondsRealtime(1f);
        canDoged = true;
    }

    private void Move()
    {
        if(DoDoged == true)
        {
            _moveGround = Input.GetAxis("Horizontal");
            _rigid.velocity = new Vector2(_moveGround * 3, _rigid.velocity.y);
        }
    }

    private void MoveAniBind()
    {
        PlayerManager.Instance.Ani.SetBool("Move", false);

        if (_moveGround <= -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            PlayerManager.Instance.Ani.SetBool("Move", true);
        }
        else if (_moveGround >= 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            PlayerManager.Instance.Ani.SetBool("Move", true);
        }
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Plat"))
        {
            PlayerManager.Instance.CanControl = true;
            OnGround = true;
            PlayerManager.Instance.Ani.SetBool("JumpEnd", true);
            PlayerManager.Instance.Ani.SetBool("Fall", false);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plat"))
        {
            OnGround = true;
            PlayerManager.Instance.Ani.SetBool("Fall", false);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plat"))
        {
            OnGround = false;
            PlayerManager.Instance.Ani.SetBool("Fall", true);
        }
    }

}
