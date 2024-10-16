using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkMech : MonoBehaviour
{
    public GameObject darkPlane;    // 이동할 Plane 오브젝트
    private Vector3 startPosition;  // 시작 위치
    private Vector3 targetPosition; // 목표 위치
    private float darkMaxY = 215f;
    
    public TimerController _timerController; // TimerController 스크립트를 참조할 변수

    void Start()
    {
        startPosition = darkPlane.transform.position;                               // Plane의 시작 위치
        targetPosition = new Vector3(startPosition.x, darkMaxY, startPosition.z);   // 목표 위치 설정
    }

    void FixedUpdate()
    {
        MovePlane(); // dark 이동 함수 호출
    }
    
    private void MovePlane()
    {
        float t = 1 - (_timerController.timeRemaining / _timerController.timeLimit); // 시간 비율 계산 (0에서 1까지)
        darkPlane.transform.position = Vector3.Lerp(startPosition, targetPosition, t); // Y좌표 이동
    }
}
