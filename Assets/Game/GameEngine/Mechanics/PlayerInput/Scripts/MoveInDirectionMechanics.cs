using UnityEngine;

public class MoveInDirectionMechanics : MonoBehaviour
{
    [SerializeField] private MoveInDirectionEngine _moveEngine;
    [SerializeField] private IntBehaviour _speed;
    [SerializeField] private Transform _transform;

    private void Update()
    {
        if(_moveEngine.IsMoving) 
        {
            MoveTransform(_moveEngine.Direction);
        }
    }

    private void MoveTransform(Vector3 direction)
    {   
        var directionXZ = new Vector3(direction.x, 0, direction.z);
        var velocity = directionXZ * (_speed.Value * Time.deltaTime);

        var vectorRelativeToPlayer = _transform.TransformVector(velocity);

        _transform.localPosition += vectorRelativeToPlayer;
    }
}
