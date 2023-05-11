using UnityEngine;

public class MovementBuilding : MonoBehaviour
{
    [SerializeField] private Transform _visualTransfrom;
    [SerializeField] private VectorEventReceiver _moveReceiver;

    private void OnEnable()
    {
        _moveReceiver.OnEvent += Move;
    }

    private void OnDisable()
    {
        _moveReceiver.OnEvent += Move;
    }

    private void Move(Vector3 obj)
    {
        _visualTransfrom.position = obj;
        var position = _visualTransfrom.position;
        position.y += 0.5f;
        _visualTransfrom.position = position;
    }
}
