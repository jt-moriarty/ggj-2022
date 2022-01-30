using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public System.Action OnDeactivate;

    [SerializeField] private UnityEvent OnInteract = new UnityEvent();
    [SerializeField] private UnityEvent OnEnter = new UnityEvent();
    [SerializeField] private UnityEvent OnExit = new UnityEvent();

    public virtual void Interact ()
    {
        OnInteract.Invoke();
    }

    public virtual void OnPlayerEnter ()
    {
        // stub intentionally left blank
        OnEnter.Invoke();
    }

    public virtual void OnPlayerExit ()
    {
        // stub intentionally left blank
        OnExit.Invoke();
    }

    public virtual void Deactivate ()
    {
        if (OnDeactivate != null)
        {
            OnDeactivate();
        }
    }
}