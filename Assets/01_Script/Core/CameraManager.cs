using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{

    private static CameraManager _cameraManager = null;


    CinemachineVirtualCamera _thisCam = null;
    CinemachineBasicMultiChannelPerlin _thisShake = null;

    [SerializeField] CinemachineVirtualCamera _ZoomCamera = null;
    CinemachineBasicMultiChannelPerlin _ZoomShake = null;

    private void Awake()
    {
        _cameraManager = this;
        _thisCam = GetComponent<CinemachineVirtualCamera>();
        _thisShake = _thisCam.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        _ZoomShake = _ZoomCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();

    }
    public static CameraManager Instance
    {
        get
        {
            return _cameraManager;
        }
    }
   
    
    public void ZoomOn()
    {
        _ZoomCamera.Priority = 11;
        _thisCam.Priority = 10;
    }

    public void ZoomOff()
    {
        _ZoomCamera.Priority = 10;
        _thisCam.Priority = 11;
    }


    public void Noise(float value = 0.5f)
    {
        StartCoroutine(Shake(value));
    }

    IEnumerator Shake(float value)
    {
        _ZoomShake.m_AmplitudeGain = value;
        _thisShake.m_AmplitudeGain = value;
        yield return new WaitForSeconds(0.2f);
        _ZoomShake.m_AmplitudeGain = 0;
        _thisShake.m_AmplitudeGain = 0;

    }
}
