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
            // Aqu� podr�as aumentar el par�metro de alguna manera
            parametroAumento += Time.deltaTime;

            // Aplicar el par�metro a la Cinemachine Virtual Camera
            cinemachineCamera.GetComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = parametroAumento;

        }
    }
}
