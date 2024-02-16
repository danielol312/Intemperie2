using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    PlaterCotrol movement;
    public Transform player;
    Rigidbody playerBody;
    public float influenceRange;
    public float intensity;
    public float distanceToPlayer;

    CountDownTimer countDownNow;
    Vector3 addedForce;
    public Vector3 AddedForce { get => addedForce; }

    // Start is called before the first frame update
    private void Awake()
    {
        countDownNow = FindObjectOfType<CountDownTimer>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            distanceToPlayer = Vector2.Distance(player.position, transform.position);
            addedForce = (transform.position - player.position).normalized;
            other.gameObject.GetComponent<PlaterCotrol>().blackHole = this;
            countDownNow.GetComponent<CountDown>();
            countDownNow.CountDown();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countDownNow.RestCount();
            addedForce = Vector2.zero;
        }
    }
}
