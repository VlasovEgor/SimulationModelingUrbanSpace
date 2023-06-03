using UnityEngine;

public class Component_CanMovement : MonoBehaviour, IComponent_CanMovement
{
    [SerializeField] private BoolBehavior _isMoving;
   
    public void CanMovement(bool canMovement)
    {
        _isMoving.Assign(canMovement);
    }
}
