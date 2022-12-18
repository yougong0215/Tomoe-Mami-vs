using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunsWhill : MonoBehaviour
{
    [SerializeField] List<Transform> guns = new List<Transform>();
    List<Vector3> _gunsTransform = new List<Vector3>();

    bool barrier = false;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            guns.Add(transform.GetChild(i));
            _gunsTransform.Add(guns[i].transform.localPosition);
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
        while(true)
        for(int i =0; i < guns.Count; i++)
        {
            yield return null;

            if (barrier == true)
            {
                guns[i].localEulerAngles = new Vector3(guns[i].localEulerAngles.x, guns[i].localEulerAngles.y, 0);
                guns[i].position = PlayerManager.Instance.Player.position - _gunsTransform[i] * 0.4f;
                guns[i].GetComponentInChildren<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
                
            }    
            else
            {
                guns[i].localEulerAngles = new Vector3(guns[i].localEulerAngles.x, guns[i].localEulerAngles.y, -45);
                guns[i].localPosition = _gunsTransform[i];
                guns[i].GetComponentInChildren<SpriteRenderer>().color = new Vector4(1, 1, 1, 0.4f);
            }

            if(guns.Count-i > PlayerManager.Instance.PAT.CurrentGuns())
            {
                guns[i].gameObject.SetActive(false);
            }
            else
            {
                guns[i].gameObject.SetActive(true);
            }

        }
        
    }

}
