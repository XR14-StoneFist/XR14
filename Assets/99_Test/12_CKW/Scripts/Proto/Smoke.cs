using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public bool IsLeft = true;
    public float Speed = 0.2f;

    private void Start()
    {
        // StartCoroutine(VerticalMoveRoutine());
    }

    private void Update()
    {
        Vector3 direction = IsLeft ? Vector3.left : Vector3.right;
        transform.Translate(direction * Speed * Time.deltaTime);
    }

    private IEnumerator VerticalMoveRoutine()
    {
        bool isUp = IsLeft;
        float position = 0;
        
        while (true)
        {
            float value = 0.2f * (isUp ? 1 : -1) * Time.deltaTime;
            transform.Translate(new Vector3(0, 0, value));
            position += value;
            
            if (isUp && position >= 0.3f)
                isUp = false;
            else if (!isUp && position <= -0.3f)
                isUp = true;
            yield return null;
        }
    }
}
