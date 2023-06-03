using UnityEngine;

public class Component_DestroyBuilding : MonoBehaviour, IComponent_DestroyBuilding
{
    [SerializeField] private Destroy _destroy;

    public IComponent_DestroyBuilding IComponent_DestroyBuilding
    {
        get => default;
        set
        {
        }
    }

    public Destroy Destroy
    {
        get => default;
        set
        {
        }
    }

    public void DestroyBuilding()
    {
        _destroy.DestroyBuidling();
    }
}
