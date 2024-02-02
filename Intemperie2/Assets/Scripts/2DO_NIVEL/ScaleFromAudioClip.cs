using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromAudioClip : MonoBehaviour
{
    public AudioSource source;
    public Vector3 minscale;
    public Vector3 maxscale;
    public AudioLoudnessDetection detector;

    public float loudnessSensibility=100;
    public float treshold = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = detector.GetLoudness(source.timeSamples, source.clip)*loudnessSensibility;

        if (loudness < treshold)
            loudness = 0;

        transform.localScale = Vector3.Lerp(minscale,maxscale,loudness);
    }
}
