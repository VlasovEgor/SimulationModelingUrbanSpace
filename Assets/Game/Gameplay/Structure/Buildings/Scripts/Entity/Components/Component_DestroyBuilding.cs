using UnityEngine;

public class Component_DestroyBuilding : MonoBehaviour, IComponent_DestroyBuilding
{
    [SerializeField] private Destroy _destroy;

    public void DestroyBuilding()
    {
        _destroy.DestroyBuidling();
    }
}
