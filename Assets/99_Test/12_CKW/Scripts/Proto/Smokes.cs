using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smokes : MonoBehaviour
{
    public float Speed = 1.0f;

    private bool isMoving = false;
    
    private void Update()
    {
        Vector3 direction = Vector3.up;
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMoving = true;
        }

        if (isMoving)
        {
            transform.Translate(direction * Speed * Time.deltaTime);
        }
    }
}
