using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaky : MonoBehaviour
{
    public PlaterCotrol player;
    public Cinemachine.CinemachineFreeLook cinemachineCamera;
    public float parametroAumento = 1.0f;

    void Update()
    {
        if (player.malestar < 50f)
        {
            // Aquí podrías aumentar el parámetro de alguna manera
            parametroAumento += Time.deltaTime;

            // Aplicar el parámetro a la Cinemachine Virtual Camera
            cinemachineCamera.GetComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = parametroAumento;

        }
    }
}
