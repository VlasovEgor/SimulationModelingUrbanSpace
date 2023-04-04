using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private FloatBehaviour _rotationSercetivity;
    [SerializeField] private Transform _transformRotate;
    [SerializeField] private VectorEventReceiver _rotateReceiver;

    private void OnEnable()
    {
        _rotateReceiver.OnEvent += OnRotating;
    }

    private void OnDisable()
    {
        _rotateReceiver.OnEvent -= OnRotating;
    }

    void OnRotating(Vector3 direction)
    {
        _transformRotate.rotation *= Quaternion.AngleAxis(direction.y * _rotationSercetivity.Value, Vector3.up) ;
        _transformRotate.rotation *= Quaternion.AngleAxis(direction.x * _rotationSercetivity.Value, Vector3.right);
        
        var currentRotation = _transformRotate.eulerAngles;
        currentRotation.z = 0;
        _transformRotate.rotation = Quaternion.Euler(currentRotation);
    }
}
