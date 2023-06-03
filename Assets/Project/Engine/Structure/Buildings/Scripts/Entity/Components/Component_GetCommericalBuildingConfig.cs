using UnityEngine;

public class Component_GetCommericalBuildingConfig : MonoBehaviour, IComponent_GetCommercalBuildingConfig
{

    [SerializeField] private CommericalBuildingConfig _buildingConfig;

    public IComponent_GetCommercalBuildingConfig IComponent_GetCommercalBuildingConfig
    {
        get => default;
        set
        {
        }
    }

    public CommericalBuildingConfig CommericalBuildingConfig
    {
        get => default;
        set
        {
        }
    }

    public CommericalBuildingConfig GetBuildingConfig()
    {
        return _buildingConfig;
    }
}