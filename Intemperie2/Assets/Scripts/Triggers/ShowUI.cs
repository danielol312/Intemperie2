using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUI : MonoBehaviour
{
    public GameObject uiObject;
    public GameObject hideObject;
    public float counter;

    void Start()
    {
        //uiObject.SetActive (false);
        counter = 0;
    }

    

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player" && counter==0)
        {
            uiObject.SetActive(true);
            //uiObject.SetActive(false);
            StartCoroutine("WaitForSec");
            counter++;
            Time.timeScale = 0f;

        }
        
    }
    

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        Destroy(uiObject);
        Destroy(gameObject);
    }

}
