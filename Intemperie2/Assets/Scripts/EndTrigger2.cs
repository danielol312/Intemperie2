using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger2 : MonoBehaviour
{
    public GameManager gameManager;
   
    

    private void OnTriggerEnter()
    {        
        {
            gameManager.CompletedLevel();
        }
        
    }
}
