using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip hover;
    public AudioClip click;
    
    public void HoverSound()
    {
        mySounds.PlayOneShot(hover);
    }
    public void ClickSound()
    {
        mySounds.PlayOneShot(click);
    }

}
