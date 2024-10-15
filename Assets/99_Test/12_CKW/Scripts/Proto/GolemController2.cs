using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GolemController2 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float followDistance;
    [SerializeField] private float followSpeed;
    [SerializeField] private int frameDelay;
    
    private Queue<Vector3> positionQueue = new Queue<Vector3>();
    private CharacterController _controller;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (player != null)
        {
            StoreTargetPosition();
            MoveFollowerWithQueue();
        }
    }

    private void StoreTargetPosition()
    {
        // 대상의 현재 위치를 큐에 저장
        positionQueue.Enqueue(player.transform.position);

        // 큐의 크기가 설정된 프레임 딜레이보다 크면 오래된 위치를 제거
        if (positionQueue.Count > frameDelay)
        {
            positionQueue.Dequeue(); // 오래된 위치를 큐에서 제거
        }
    }
    
    
    // 항상 왼쪽에 있는 문제를 고쳐줘야 함
    
    private void MoveFollowerWithQueue()
    {
        if (positionQueue.Count >= frameDelay)
        {
            Vector3 targetPosition = positionQueue.Peek() - new Vector3(followDistance, 0, 0);
            
            Vector3 direction = (targetPosition - transform.position).normalized;
            float moveDistance = followSpeed * Time.deltaTime;
            _controller.Move(direction * moveDistance);
        }
    }
}
