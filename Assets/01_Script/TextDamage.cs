using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDamage : PoolAble
{
    public void Seting(string a)
    {
        GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
        GetComponent<TextMeshPro>().text = a;
    }

    private void Update()
    {
        GetComponent<RectTransform>().localScale -= new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;

        if(GetComponent<RectTransform>().localScale.x < 0)
        {
            PoolManager.Instance.Push(this);
        }
    }
}
