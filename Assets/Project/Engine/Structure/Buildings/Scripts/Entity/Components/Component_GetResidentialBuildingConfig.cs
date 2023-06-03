using UnityEngine;

public class Component_GetResidentialBuildingConfig : MonoBehaviour, IComponent_GetResidentialBuildingConfig
{
    [SerializeField] private ResidentialBuildingConfig _buildingConfig;

    public IComponent_GetResidentialBuildingConfig IComponent_GetResidentialBuildingConfig
    {
        get => default;
        set
        {
        }
    }

    public ResidentialBuildingConfig ResidentialBuildingConfig
    {
        get => default;
        set
        {
        }
    }

    public ResidentialBuildingConfig GetBuildingConfig()
    {
        return _buildingConfig;
    }
}
