using UnityEngine;

public class Component_GetResidentialBuildingConfig : MonoBehaviour, IComponent_GetResidentialBuildingConfig
{
    [SerializeField] private ResidentialBuildingConfig _buildingConfig;

    public ResidentialBuildingConfig GetBuildingConfig()
    {
        return _buildingConfig;
    }
}
