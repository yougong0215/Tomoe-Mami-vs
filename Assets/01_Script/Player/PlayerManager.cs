using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Animator Ani;
    public SpriteRenderer Spi;
    public PlayerAttack PAT;

    public LayerMask layer;

    bool _con= true;
    public bool CanControl
    {
        get => _con;
        set => _con = value;
    }

    private void Awake()
    {
        Ani = GameObject.FindGameObjectWithTag("Sprite").GetComponent<Animator>();
        Spi = GameObject.FindGameObjectWithTag("Sprite").GetComponent<SpriteRenderer>();
        PAT = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        layer = Player.gameObject.layer;
        
    }
    Transform _player = null;
    public Transform PlayerS
    {
        get
        {
            if(_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Sprite").transform;
            }
            return _player;
        }
    }
    Transform _player2 = null;
    public Transform Player
    {
        get
        {
            if (_player2 == null)
            {
                _player2 = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _player2;
        }
    }

    public int CurrentGuns()
    {
        return PAT.CurrentGuns;
    }
}
