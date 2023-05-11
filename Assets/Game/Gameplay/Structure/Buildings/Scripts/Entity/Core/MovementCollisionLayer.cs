using UnityEngine;

public class MovementCollisionLayer : MonoBehaviour
{
    [SerializeField] private Transform _collisionTransform;
    [SerializeField] private Transform _visualTransform;

    void Update()
    {
        MovementCollision();
    }

    private void MovementCollision()
    {   
        _collisionTransform.rotation = _visualTransform.rotation;

        var positionX = _visualTransform.position.x;
        var positionZ = _visualTransform.position.z;
        var positionY = 0.5f;

        Vector3 newPosition = new Vector3(positionX, positionY, positionZ);

        _visualTransform.position = newPosition;
        _collisionTransform.position = newPosition;
    }
}
