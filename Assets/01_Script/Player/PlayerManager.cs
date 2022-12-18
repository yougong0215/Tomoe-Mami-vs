using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Animator Ani;
    public SpriteRenderer Spi;
    public PlayerAttack PAT;

    private void Awake()
    {
        Ani = GameObject.FindGameObjectWithTag("Sprite").GetComponent<Animator>();
        Spi = GameObject.FindGameObjectWithTag("Sprite").GetComponent<SpriteRenderer>();
        PAT = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
    }
    Transform _player = null;
    public Transform Player
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

    public int CurrentGuns()
    {
        return PAT.CurrentGuns();
    }
}
