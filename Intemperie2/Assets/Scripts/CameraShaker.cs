using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    float ShakeIntensity = 1f;
    private float ShakeTime = 0.2f;

    private float timer;
    private CinemachineBasicMultiChannelPerlin _cmbmp;


    private void Start()
    {
        StopShake();
    }

    private void Awake()
    {
        CinemachineVirtualCamera= GetComponent<CinemachineVirtualCamera>();
    }
    private void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin _cmbmp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cmbmp.m_AmplitudeGain = ShakeIntensity;

        timer = ShakeTime;
    }
    private void StopShake()
    {
        CinemachineBasicMultiChannelPerlin _cmbmp = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _cmbmp.m_AmplitudeGain = 0f;

        timer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ShakeCamera();
        }
        if (timer > 0)
        {
            timer-= Time.deltaTime;
            if(timer <= 0)
            {
                StopShake();
            }
        }
    }
}
