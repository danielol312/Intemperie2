using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;
    Collectible recolecta;
    public GameObject intentaDeNuevo;
    
    private void OnTriggerEnter()
    {
        //if (Collectible.total == 6)
        {
            gameManager.CompletedLevel();
        }
        /*else
        {
            intentaDeNuevo.gameObject.SetActive(true);
        }*/
    }
}
