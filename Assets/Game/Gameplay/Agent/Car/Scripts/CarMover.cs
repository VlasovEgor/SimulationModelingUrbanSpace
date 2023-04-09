using UnityEngine;
using Zenject;

public class CarMover : MonoBehaviour
{
    [SerializeField] private Transform _carTransfrom;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _movementVector;

    [SerializeField] private CarAgent _carAI;

    private void Start()
    {
        _carAI.OnDrive += SetMovementVector;
    }

    private void OnDestroy()
    {
        _carAI.OnDrive -= SetMovementVector;
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
        _carTransfrom.position = Vector3.MoveTowards(_carTransfrom.position,_movementVector, step);

        var lookDirection = _movementVector - _carTransfrom.transform.position;
        _carTransfrom.rotation = Quaternion.LookRotation(lookDirection);
    }
}
