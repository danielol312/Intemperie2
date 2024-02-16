using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollission : MonoBehaviour
{
    public GameObject deathEffect;

    public PlaterCotrol movement;

    


    private void OnCollisionEnter(Collision collission)
    {
        if(collission.collider.tag=="Enemy")
        {

            Debug.Log(collission.collider.name);

            FindAnyObjectByType<GameManager>().EndGame();

            movement.enabled = false;

            Instantiate(deathEffect,transform.position, transform.rotation);
            //GameManager.instance.EndGame();

            FindAnyObjectByType<AudioManager>().Play("Death");

            deathEffect.gameObject.SetActive(true);

            //Destroy(gameObject);
        }

        else if (collission.collider.tag == "Collectable")
        {
            FindAnyObjectByType<AudioManager>().Play("Collectible");
        }
    }
}
