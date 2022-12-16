using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Animator Ani;
    public SpriteRenderer Spi;

    private void Awake()
    {
        Ani = GameObject.FindGameObjectWithTag("Sprite").GetComponent<Animator>();
        Spi = GameObject.FindGameObjectWithTag("Sprite").GetComponent<SpriteRenderer>();
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
}
