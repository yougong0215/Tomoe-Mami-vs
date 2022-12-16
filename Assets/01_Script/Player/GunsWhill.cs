using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunsWhill : MonoBehaviour
{
    [SerializeField] List<Transform> guns = new List<Transform>();

    bool barrier = false;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            guns.Add(transform.GetChild(i));
        }
        StartCoroutine(Whil());
    }
    private void Update()
    {
        transform.localEulerAngles += new Vector3(0, 20, 0) * Time.deltaTime;

        if(Input.GetKey(KeyCode.F))
        {
            barrier = true;
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            barrier= false;
        }

    }


    IEnumerator Whil()
    {
        yield return null;
        for(int i =0; i < guns.Count; i++)
        {
            if(barrier == true)
            {
                guns[i].localEulerAngles = new Vector3(guns[i].localEulerAngles.x, guns[i].localEulerAngles.y, 0);
            }    
            else
            {
                guns[i].localEulerAngles = new Vector3(guns[i].localEulerAngles.x, guns[i].localEulerAngles.y, -45);
            }
        }
        
        StartCoroutine(Whil());
    }

}
