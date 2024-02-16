using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenCvSharp;

public class GetCameraImage : MonoBehaviour
{
    [SerializeField] private RawImage img = default;
    WebCamTexture webCam;

    CascadeClassifier cascade;
    OpenCvSharp.Rect myFace;



    private void Start()
    {
        webCam = new WebCamTexture();
        if(!webCam.isPlaying) webCam.Play();
        img.texture = webCam;

        
        cascade = new CascadeClassifier(Application.dataPath+ @"haarcascade_frontalface_default.xml");
    }

    private void Update()
    {
        Mat frame =OpenCvSharp.Unity.TextureToMat(webCam);

        findNewFace(frame);
        display(frame);
    }

    void findNewFace(Mat frame)
    {
        var faces= cascade.DetectMultiScale(frame,1.1,2,HaarDetectionType.ScaleImage);

        if (faces.Length >= 1)
        {
            Debug.Log(faces[0].Location);
            myFace = faces[0];
        }
    }

    void display(Mat frame)
    {
        if(myFace != null)
        {
            frame.Rectangle(myFace, new Scalar(250, 0, 0), 2);
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
