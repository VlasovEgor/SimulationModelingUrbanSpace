using UnityEngine;

public class Component_MoveInDirection : MonoBehaviour, IComponent_MoveInDirection
{
    [SerializeField] private MoveInDirectionEngine _moveEngine;

    public void MoveInDirection(Vector3 direction)
    {
        _moveEngine.Move(direction);
    }
}
