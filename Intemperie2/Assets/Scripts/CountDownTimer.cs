using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    float currentTime=0f;
    float startingTime=10f;
    

    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] GameObject objetoTexto;

    void Start()
    {
        currentTime = startingTime;
    }
    void Update()
    {
        Debug.Log("counting");
        if (currentTime == 0f)
        {
            FindAnyObjectByType<GameManager>().EndGame();
        }
    }
    public void CountDown()
    {
        Debug.Log("time down");
        currentTime -= 1 * Time.deltaTime;
        countDownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
    public void RestCount()
    {
        currentTime = startingTime;
        countDownText.text = currentTime.ToString("0");
    }
}
