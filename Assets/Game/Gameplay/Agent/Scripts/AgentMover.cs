using UnityEngine;

public class AgentMover : MonoBehaviour
{
    [SerializeField] private Transform _agentTransfrom;
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _movementVector;

    [SerializeField] private Agent _humanAI;

    private void Start()
    {
        _humanAI.OnMove += SetMovementVector;
    }

    private void OnDestroy()
    {
        _humanAI.OnMove -= SetMovementVector;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void SetMovementVector(Vector3 movementVector)
    {
        _movementVector = movementVector;
    }

    private void Movement()
    {
        var step = _speed * Time.deltaTime;

        _agentTransfrom.position = Vector3.MoveTowards(_agentTransfrom.position, _movementVector, step);
        _agentTransfrom.rotation = Quaternion.LookRotation(_movementVector);
    }
}
