using UnityEngine;

public class Component_Rotate : MonoBehaviour, IComponent_Rotate
{
    [SerializeField] private VectorEventReceiver _rotateReceiver;

    public void Rotate(Vector2 vectorRotate)
    {
        _rotateReceiver.Offset(vectorRotate);
    }
}
