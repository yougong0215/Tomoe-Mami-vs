using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// GunHoldShoot

public class GunHoldShoot : PoolAble
{
    public static bool shoot = false;

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
        transform.localEulerAngles = Vector3.zero;
        //transform.localEulerAngles = Vector3.zero;
        StartCoroutine(EnemyLookOn());
    }

    private void Update()
    {
        if (rot == true)
        {
            this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle - 270, Vector3.forward), Time.deltaTime * 50);
        }
    }

    IEnumerator EnemyLookOn()
    {
        mouse = Input.mousePosition;
        mouse.z = GameManager.Instance.Cam.farClipPlane;
        //transform.position = Camera.main.ScreenToWorldPoint(mouse);
        Ray ray = GameManager.Instance.Cam.ScreenPointToRay(mouse);

        RaycastHit hit;
        yield return null;
        rot = true;
        if (Physics.Raycast(ray, out hit))
        {
            angle = Mathf.Atan2(hit.point.y - transform.position.y, hit.point.x - transform.position.x) * Mathf.Rad2Deg;

            yield return null;


            //Debug.Log(transform.localEulerAngles);

            _spi.flipX = transform.localEulerAngles.z > 180 ? true : false;
            _spi.sortingOrder = Random.Range(-1, 3);
        }
        yield return new WaitUntil(()=>shoot == true);
        yield return new WaitForSeconds(1f);
        Debug.Log("≈¡≈∏≈∏≈¡");
        transform.position += (transform.position - hit.point).normalized;

        CameraManager.Instance.Noise(1);

        yield return new WaitForSeconds(0.8f);
        shoot = false;
        PoolManager.Instance.Push(this);
    }
}
