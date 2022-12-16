using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveGround = 0;
    Rigidbody _rigid;
    bool canDoged = true;
    bool DoDoged = true;
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



        //VelocityBind();

        if(PlayerAttack.shot == true)
        {
            _moveGround = 0;
            _rigid.velocity = new Vector3(0, -0.025f, 0);
            return;
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
        if(_rigid.velocity.x > 3.5f)
        {
            _rigid.velocity += new Vector3(-300f, 0, 0) * Time.deltaTime;
        }
        else if (_rigid.velocity.x < -3.5f)
        {
            _rigid.velocity += new Vector3(300f, 0, 0) * Time.deltaTime;
        }

        if(_rigid.velocity.y < -6)
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
                if (PlayerManager.Instance.Spi.flipX) // <<
                {
                    transform.position += new Vector3(0.0004f, 0, 0);
                }
                else // >>
                {
                    transform.position += new Vector3(-0.004f, 0, 0);
                }
            }
            else
            {
                if (PlayerManager.Instance.Spi.flipX) // <<
                {
                    transform.position += new Vector3(-0.03f, 0, 0);
                }
                else // >>
                {
                    transform.position += new Vector3(0.03f, 0, 0);
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
            transform.position += new Vector3(_moveGround * 3, 0,0) * Time.deltaTime;
        }
    }

    private void MoveAniBind()
    {
        PlayerManager.Instance.Ani.SetBool("Move", false);

        if (_moveGround <= -0.1f)
        {
            PlayerManager.Instance.Spi.flipX = true;
            PlayerManager.Instance.Ani.SetBool("Move", true);
        }
        else if (_moveGround >= 0.1f)
        {
            PlayerManager.Instance.Spi.flipX = false;
            PlayerManager.Instance.Ani.SetBool("Move", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Plat"))
        {
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
