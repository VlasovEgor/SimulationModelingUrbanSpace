using UnityEngine;

public class Component_CameraRotate : MonoBehaviour, IComponent_CameraRotate
{
    [SerializeField] private VectorEventReceiver _rotateReceiver;

    public void Rotate(Vector2 vectorRotate)
    {
        _rotateReceiver.Offset(vectorRotate);
    }
}
