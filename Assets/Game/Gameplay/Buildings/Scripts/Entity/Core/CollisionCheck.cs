using System;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField] private EventReceiver_Trigger _conflictTrigger;

    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeField] private Material _defaultBilding;
    [SerializeField] private Material _conflictBilding;

    private void OnEnable()
    {
        _conflictTrigger.OnTriggerEntered+= TriggerWithObject;
        _conflictTrigger.OnTriggerExited += TriggerWithObjectHasPassed;
    }
    private void OnDisable()
    {
        _conflictTrigger.OnTriggerEntered -= TriggerWithObject;
        _conflictTrigger.OnTriggerExited -= TriggerWithObjectHasPassed;
    }

    private void TriggerWithObject(Collider obj)
    {
        _meshRenderer.material = _conflictBilding;
    }

    private void TriggerWithObjectHasPassed(Collider obj)
    {
        _meshRenderer.material = _defaultBilding;
    }
}
