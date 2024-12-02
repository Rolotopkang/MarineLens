using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    public bool hasbeenchecked = false;
    public UnityEvent TriggerEnterEvent;
    public Action Triggeraction;
    public UnityEvent TriggerExitEvent;
    public UnityEvent OnUIEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasbeenchecked)
        {
            TriggerEnterEvent.Invoke();
            Triggeraction.Invoke();
            hasbeenchecked = true;
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         TriggerExitEvent.Invoke();
    //     }
    // }
}
