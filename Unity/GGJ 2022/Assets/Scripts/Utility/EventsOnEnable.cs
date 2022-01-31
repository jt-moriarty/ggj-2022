using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventsOnEnable : MonoBehaviour
{
    [SerializeField] private UnityEvent enableEvents;

    void OnEnable ()
    {
        Debug.Log("enable events");
        enableEvents.Invoke();
    }
}
