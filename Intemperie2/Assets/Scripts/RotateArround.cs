using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RotateArround : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public float rotateSpeed = 30f;
    public PlaterCotrol movement;

    void Update()
    {
        
        player.transform.RotateAround(target.position, Vector3.up, rotateSpeed * Time.deltaTime);

    }



}
