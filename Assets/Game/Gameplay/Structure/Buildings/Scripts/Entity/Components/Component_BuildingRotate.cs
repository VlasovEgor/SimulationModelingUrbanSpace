using UnityEngine;

public class Component_BuildingRotate : MonoBehaviour, IComponent_BuildingRotate
{
    [SerializeField] private IntEventReceiver _direction;

    public void Rotate(int direction)
    {
        _direction.Call(direction);
    }
}
