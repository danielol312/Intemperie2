using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Introduccion : MonoBehaviour
{
    public GameObject cargando;
    //public string scenename;
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Lvl1");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Lvl1");
    }

    public void Activar()
    {
       cargando.SetActive(true);
    }
    

}
