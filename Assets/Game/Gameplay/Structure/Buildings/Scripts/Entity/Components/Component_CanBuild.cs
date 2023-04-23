using UnityEngine;

public class Component_CanBuild : MonoBehaviour, IComponent_CanBuild
{
    [SerializeField] private BuildingEngine _buildingEngine;

    public bool CanBuild()
    {
        return _buildingEngine.CanBuild();
    }
}
