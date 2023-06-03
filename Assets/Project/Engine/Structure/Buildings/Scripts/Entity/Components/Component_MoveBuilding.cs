using UnityEngine;

public class Component_MoveBuilding : MonoBehaviour, IComponent_MoveBuilding
{
    [SerializeField] private VectorEventReceiver _moveReceiver;

    public IComponent_MoveBuilding IComponent_MoveBuilding
    {
        get => default;
        set
        {
        }
    }

    public void MoveBuilding(Vector3 direction)
    {
        _moveReceiver.Offset(direction);
    }
}
