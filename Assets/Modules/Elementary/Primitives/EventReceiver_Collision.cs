using System;
using UnityEngine;


[DisallowMultipleComponent]
public sealed class EventReceiver_Collision : MonoBehaviour
{
    public event Action<Collision> OnCollisionEntered;

    public event Action<Collision> OnCollisionStaying;

    public event Action<Collision> OnCollisionExited;

    private void OnCollisionEnter(Collision collision)
    {   
        this.OnCollisionEntered?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("PIZDEC");
        this.OnCollisionStaying?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        this.OnCollisionExited?.Invoke(collision);
    }
}
