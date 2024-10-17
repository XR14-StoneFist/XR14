using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public float timeLimit = 60f;   // 제한 시간 (초)
    public float timeRemaining;
    private bool isTiming = false;

    private TextMesh timerText;     // TextMesh 컴포넌트를 사용할 변수
    
    void Start()
    {
        timeRemaining = timeLimit;
        timerText = GetComponent<TextMesh>(); // TextMesh 컴포넌트 가져오기
        
        StartTimer();
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        if (isTiming)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTiming = false;
                GameOver();
            }

        }
    }

    public void StartTimer() // 게임 시작시 호출
    {
        isTiming = true;
    }

    public void StopTimer() // 게임 종료시 호출
    {
        isTiming = false;
    }


    private void GameOver()
    {
        Debug.Log("게임 오버! 시간 초과!");
    }
}
