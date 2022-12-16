using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    // Made by GmSoft ( You Jae Hyun )


    [Header("��¥ ī�޶�� ��ġ�� ��¥ ī�޶� ����")]
    [SerializeField] Transform _vcam;
    [SerializeField] Transform _vcamFake;

    [Header("ī�޶� ����")]
    [SerializeField] float CameraMaxDistance = 5f;
    bool _setPos = false;

    int L = 1, U = 1;

    RaycastHit hit;
    Vector3 _hitVec;

    float shakeDuration = 0;
    float shakeAmount = 0.05f;
    float decreaseFactor = 1f;

    /// <summary>
    /// ī�޶� ����ŷ
    /// </summary>
    /// <param name="a"></param> ���ӽð�
    /// <param name="b"></param> ������
    /// <param name="c"></param> ���� %
    public void shaking(float a = 0.1f, float b = 0.05f, float c = 1f)
    {
        shakeDuration = a;
        shakeAmount = b;
        decreaseFactor = c;

    }

    private Transform _player;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.Find("Player").GetComponent<Transform>();
            }
            return _player;
        }
    }


    private void LateUpdate()
    {

        CameraAltitude();
        shake();
    }

    void CameraAltitude()
    {

        // �÷��̾� ��ġ�� ���� �߹ٴ��� + 1.4f
        transform.position = Player.position + new Vector3(0, 1.4f, 0);

        // ���� �־�� �� ��ġ�� �̵��ϴ� ķ
        _hitVec = _vcamFake.transform.position;

        if (_setPos == false)
        {
            _hitVec = _vcamFake.transform.position;
        }

        if (Physics.Raycast(transform.position, (_vcamFake.transform.position - transform.position).normalized, out hit, CameraMaxDistance))
        {
            _setPos = true;
            _hitVec = hit.point;//Vector3.Lerp(_vcamFake.transform.position, hit.point, Time.deltaTime * 1500f);

        }
        else if (Vector3.Distance(transform.position, _vcam.transform.position) > Vector3.Distance(transform.position, _vcamFake.transform.position))
        {
            _setPos = false;
            _hitVec = _vcamFake.transform.position;
        }
    }


    void shake()
    {
        if (shakeDuration > 0)
        {
            _vcam.transform.position = _hitVec + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

}

