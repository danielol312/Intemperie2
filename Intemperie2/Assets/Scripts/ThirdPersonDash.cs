using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonDash : MonoBehaviour
{
    PlaterCotrol moveScript;

    public float dashSpeed;
    public float dashTime;

    void Start()
    {
        moveScript = GetComponent<PlaterCotrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dash());
            Debug.Log("Dashed");
        }
    }

    IEnumerator Dash()
    {
        float startTime= Time.time;

        while( Time.time < startTime + dashTime )
        {
            moveScript.controller.Move(moveScript.playerVelocity * dashSpeed);

            yield return null;
        }
    }
}
