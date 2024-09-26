using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTriggerColider : MonoBehaviour
{
    public Action<string> A_OnTriggerEnter;
    public Action<string> A_OnTriggerExit;
    public Action<string> A_OnTriggerStay;
    public Action<string> A_OnCollisionEnter;
    public Action<string> A_OnCollisionExit;
    public Action<string> A_OnCollisionStay;

    private void OnTriggerEnter(Collider other)
    {
        A_OnTriggerEnter?.Invoke(other.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        A_OnTriggerExit?.Invoke(other.tag);

    }
    private void OnTriggerStay(Collider other)
    {
        A_OnTriggerStay?.Invoke(other.tag);

    }
    private void OnCollisionEnter(Collision collision)
    {
        A_OnCollisionEnter?.Invoke(collision.transform.tag);

    }
    private void OnCollisionExit(Collision collision)
    {
        A_OnCollisionExit?.Invoke(collision.transform.tag);

    }
    private void OnCollisionStay(Collision collision)
    {
        A_OnCollisionStay?.Invoke(collision.transform.tag);

    }
}
