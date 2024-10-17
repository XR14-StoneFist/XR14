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
    
    [Header("Damage")]
    [SerializeField] private SubCollider DamageCollider;

    [SerializeField] private float DamageCooldown;

    private float _currentDamageCooldown = 0;

    private void Start()
    {
        StartCoroutine(ChaseRoutine());

        DamageCollider.OnCollisionStayAction += tagName =>
        {
            if (tagName == "Player" && _currentDamageCooldown <= 0)
            {
                GameManager.Instance.Health--;
                _currentDamageCooldown = DamageCooldown;
            }
        };
    }

    private void Update()
    {
        _currentDamageCooldown -= Time.deltaTime;
    }

    private IEnumerator ChaseRoutine()
    {
        while (true)
        {
            float chasePosition = Player.transform.position.y + Random.Range(ChaseMinValue, ChaseMaxValue);
            float chaseTimeLeft = ChaseTime;
            float chaseSpeed = (chasePosition - transform.position.y) / ChaseTime;
            while (chaseTimeLeft > 0)
            {
                transform.Translate(Vector3.up * (chaseSpeed + ChaseSpeed) * Time.deltaTime);
                chaseTimeLeft -= Time.deltaTime;
                yield return null;
            }
        }
    }
}
