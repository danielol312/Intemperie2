using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
   

    public void ChangeScene(int loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
