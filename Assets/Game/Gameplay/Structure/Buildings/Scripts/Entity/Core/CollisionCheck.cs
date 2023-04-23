using System;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public event Action BuildingCollided;
    public event Action CollidedBuildingsHasPassed;

    [SerializeField] private EventReceiver_Trigger _conflictTrigger;

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
        BuildingCollided?.Invoke();
    }

    private void TriggerWithObjectHasPassed(Collider obj)
    {
        CollidedBuildingsHasPassed?.Invoke();
    }
}
