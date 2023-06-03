using Entities;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] private Agent _agent; 
    [SerializeField] private GameObject _raycastStartingPoint = null;
    [SerializeField] private float _collisionRaycastLength = 0.1f;

    public Agent Agent
    {
        get => default;
        set
        {
        }
    }

    private void Update()
    {
       // CheckForCollision();
    }

    private void CheckForCollision()
    {   
        Ray ray = new Ray(_raycastStartingPoint.transform.position, _raycastStartingPoint.transform.forward * _collisionRaycastLength);
        RaycastHit raycastHit;

        if(Physics.Raycast(ray,out raycastHit))
        {
            if(raycastHit.collider.GetComponent<UnityEntityProxy>())
            {
                _agent.SetMove(false);
            }
            else
            {
                _agent.SetMove(true);
            }
        }
    }
}
