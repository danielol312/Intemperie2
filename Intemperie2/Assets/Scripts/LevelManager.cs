using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
   
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
   // public async void LoadScene(string sceneName)
    //{
        //var scene = SceneManager.LoadSceneAsync(sceneName);
        //scene.allowSceneActivation = false;
   // }
}
