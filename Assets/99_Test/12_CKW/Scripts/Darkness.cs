using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Darkness : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float ChaseMinValue;
    [SerializeField] private float ChaseMaxValue;
    [SerializeField] private float ChaseSpeed;
    [SerializeField] private float ChaseTime;

    private void Start()
    {
        StartCoroutine(ChaseRoutine());
    }

    private IEnumerator ChaseRoutine()
    {
        float chasePosition = Player.transform.position.y + Random.Range(ChaseMinValue, ChaseMaxValue);
        float chaseTimeLeft = ChaseTime;
        float chaseSpeed = chasePosition / ChaseTime;
        while (chaseTimeLeft > 0)
        {
            transform.Translate(Vector3.up * (chaseSpeed + ChaseSpeed) * Time.deltaTime);
            chaseTimeLeft -= Time.deltaTime;
            yield return null;
        }
        
        StartCoroutine(ChaseRoutine());
    }
}
