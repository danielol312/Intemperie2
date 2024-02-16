
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded=false;
    public float delay= 1f;
    public string nextScene;
    public GameObject completeLevelUI;
    public GameObject GameOver;
    public void CompletedLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(nextScene);
    }


    public void EndGame()
    {
        if (gameHasEnded==false)
        {
            gameHasEnded = true;
            Debug.Log("GameOver");
            //Invoke("Restart",delay);
            GameOver.SetActive(true);
        }
        
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
