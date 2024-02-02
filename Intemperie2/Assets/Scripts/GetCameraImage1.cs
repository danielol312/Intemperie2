using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class GetCameraImage1 : MonoBehaviour
{
   
    WebCamTexture webCamTexture;

    CascadeClassifier _cascade;
    OpenCvSharp.Rect _myFace;



    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        webCamTexture = new WebCamTexture(devices[0].name);
        webCamTexture.Play();


        _cascade = new CascadeClassifier(Application.dataPath+@"haarcascade_frontalface_default.xml");
    }

    private void Update()
    {
        Mat frame =OpenCvSharp.Unity.TextureToMat(webCamTexture);

        _findNewFace(frame);
        _display(frame);
    }

    void _findNewFace(Mat frame)
    {
        var faces= _cascade.DetectMultiScale(frame,1.1,2,HaarDetectionType.ScaleImage);

        if (faces.Length >= 1)
        {
            Debug.Log(faces[0].Location);
            _myFace = faces[0];
        }
    }

    void _display(Mat frame)
    {
        if(_myFace != null)
        {
            frame.Rectangle(_myFace, new Scalar(250, 0, 0), 2);
        }

        Texture newTexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newTexture;
    }



    //WebCamTexture webCamTexture;
    //public string path;
    //public RawImage imgDisplayForPhotoSnap;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    webCamTexture= new WebCamTexture();
    //    GetComponent<Renderer>().material.mainTexture = webCamTexture;
    //    webCamTexture.Play();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
