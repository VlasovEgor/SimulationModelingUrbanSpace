using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMover : MonoBehaviour
{
    [SerializeField] private Transform _humanTransfrom;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _movementVector;

    [SerializeField] private HumanAgent _humanAI;

    private void Start()
    {
        _humanAI.OnMove += SetMovementVector;
    }

    private void OnDestroy()
    {
        _humanAI.OnMove -= SetMovementVector;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void SetMovementVector(Vector3 movementVector)
    {
        _movementVector = movementVector;
    }

    private void Movement()
    {
        var step = _speed * Time.deltaTime;

        _humanTransfrom.position = Vector3.MoveTowards(_humanTransfrom.position, _movementVector, step);
        _humanTransfrom.rotation = Quaternion.LookRotation(_movementVector);
    }
}
