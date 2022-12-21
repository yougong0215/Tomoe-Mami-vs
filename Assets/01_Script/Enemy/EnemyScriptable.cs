using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "적")]
public class EnemyScriptable : ScriptableObject
{
    [Header("기본 정보")]
    public float HP = 10000;
    public float Attack = 5;
    public float Barrier = 5;
    public float speed = 2;
    public float JumpPower = 5;
    public float pase = 1;

    [Header("스킬")]
    [SerializeField] public List<skill> skills = new List<skill>();

    private void OnEnable()
    {
        HP = 10000;
    }
}


[System.Serializable]
public class skill
{
    public string skill1 = "";
    public float CoolTime = 3;
}