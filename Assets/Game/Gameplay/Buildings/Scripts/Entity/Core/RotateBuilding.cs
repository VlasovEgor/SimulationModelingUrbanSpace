using UnityEngine;

public class RotateBuilding : MonoBehaviour
{
    [SerializeField] private IntEventReceiver _rotateReceiver;
    [SerializeField] private IntBehaviour _speedRotation;
    [SerializeField] private Transform _transformRotate;


    private void OnEnable()
    {
        _rotateReceiver.OnEvent += OnRotating;
    }

    private void OnDisable()
    {
        _rotateReceiver.OnEvent -= OnRotating;
    }

    void OnRotating(int direction)
    {
        _transformRotate.rotation *= Quaternion.AngleAxis(direction * _speedRotation.Value * Time.deltaTime, Vector3.up);
    }
}
