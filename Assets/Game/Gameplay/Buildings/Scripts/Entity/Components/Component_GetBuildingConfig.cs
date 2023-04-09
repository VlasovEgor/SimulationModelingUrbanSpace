using UnityEngine;

public class Component_GetBuildingConfig : MonoBehaviour, IComponent_GetBuildingConfig
{
    [SerializeField] private BuildingConfig _buildingConfig;

    public BuildingConfig GetBuildingConfig()
    {
        return _buildingConfig;
    }
}