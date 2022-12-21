using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunState : PoolAble
{
    [SerializeField] Transform pos;

    float angle = 90;
    float _time = 1;
    bool rot = false;
    Vector3 mouse;
    SpriteRenderer _spi;
    private void Awake()
    {
        _spi = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        rot = false;
        angle = 0;
        _time = 1;
        transform.localEulerAngles = Vector3.zero;
        StartCoroutine(EnemyLookOn());
    }

    private void Update()
    {
        if(rot == true)
            this.transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.AngleAxis(angle - 270, Vector3.forward), Time.deltaTime * 10);
        _time -= Time.deltaTime;
    }

    IEnumerator EnemyLookOn()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        mouse = Input.mousePosition;
        mouse.z = GameManager.Instance.Cam.farClipPlane;
        //transform.position = Camera.main.ScreenToWorldPoint(mouse);
        Ray ray = GameManager.Instance.Cam.ScreenPointToRay(mouse);
        RaycastHit hit;

        yield return new WaitForSeconds(0.6f);

        rot = true;
        if(Physics.Raycast(ray, out hit))
        {
            angle = Mathf.Atan2(hit.point.y - transform.position.y, hit.point.x - transform.position.x) * Mathf.Rad2Deg;
            
            yield return null;
            //Debug.Log(transform.localEulerAngles);

            _spi.flipX = transform.localEulerAngles.z > 180 ? true : false;
            _spi.sortingOrder = Random.Range(-1, 3);
        }

        yield return new WaitUntil(()=> transform.rotation == Quaternion.AngleAxis(angle - 270, Vector3.forward) || _time < 0);
        //Debug.Log("ÅÁ");
        PlayerBullet p = PoolManager.Instance.Pop("PlayerBullet") as PlayerBullet;
        p.transform.rotation = transform.rotation;
        p.transform.position = transform.position;
        CameraManager.Instance.Noise(0.2f);

        yield return new WaitForSeconds(0.1f);
        CameraManager.Instance.Noise(0.4f);
        PoolManager.Instance.Push(this);
    }
}
