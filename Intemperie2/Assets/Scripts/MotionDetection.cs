using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenCvSharp.Util;
using System.Collections;
using UnityEngine;
using OpenCvSharp;

public class MotionDetection : MonoBehaviour
{
    WebCamTexture webCamTexture;

    
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        webCamTexture = new WebCamTexture(devices[0].name);
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
    }
}
