using System;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Sensor : MonoBehaviour
{
    [Foldout("EVENTS")] public UnityEvent<Transform> OnEntityEnterUE;
    [Foldout("EVENTS")] public UnityEvent<Vector3> OnEntityExitUE;
    public event Action<Transform> OnEntityEnter;
    public event Action<Vector3> OnEntityExit;

    protected void InvokeEnterEvents(Transform obj)
    {
        OnEntityEnter?.Invoke(obj.transform);
        OnEntityEnterUE.Invoke(obj.transform);
    }

    protected void InvokeExitEvents(Transform obj)
    {
        OnEntityExit?.Invoke(obj.position);
        OnEntityExitUE.Invoke(obj.position);
    }
}