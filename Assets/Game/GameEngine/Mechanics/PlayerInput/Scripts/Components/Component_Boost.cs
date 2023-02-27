using UnityEngine;

public class Component_Boost : MonoBehaviour, IComponent_Boost
{
    [SerializeField] private BoolBehavior _boostBehavior;
    
    public void CanBoost(bool isBoost)
    {
        _boostBehavior.Assign(isBoost);
    }
}
