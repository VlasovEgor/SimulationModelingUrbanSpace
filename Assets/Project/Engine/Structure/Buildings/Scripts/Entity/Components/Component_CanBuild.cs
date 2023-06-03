using UnityEngine;

public class Component_CanBuild : MonoBehaviour, IComponent_CanBuild
{
    [SerializeField] private BuildingEngine _buildingEngine;

    public IComponent_CanBuild IComponent_CanBuild
    {
        get => default;
        set
        {
        }
    }

    public bool CanBuild()
    {
        return _buildingEngine.CanBuild();
    }
}
