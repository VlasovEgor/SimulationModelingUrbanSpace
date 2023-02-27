using UnityEngine;

public class Component_MoveBuilding : MonoBehaviour, IComponent_MoveBuilding
{
    [SerializeField] private VectorEventReceiver _moveReceiver;

    public void MoveBuilding(Vector3 direction)
    {
        _moveReceiver.Offset(direction);
    }
}
