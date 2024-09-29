using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCollider : MonoBehaviour
{
    public Action<string> OnTriggerEnterAction;
    public Action<string> OnTriggerExitAction;
    public Action<string> OnTriggerStayAction;
    public Action<string> OnCollisionEnterAction;
    public Action<string> OnCollisionExitAction;
    public Action<string> OnCollisionStayAction;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterAction?.Invoke(other.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitAction?.Invoke(other.tag);

    }
    private void OnTriggerStay(Collider other)
    {
        OnTriggerStayAction?.Invoke(other.tag);

    }
    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterAction?.Invoke(collision.transform.tag);

    }
    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitAction?.Invoke(collision.transform.tag);

    }
    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayAction?.Invoke(collision.transform.tag);

    }
}
