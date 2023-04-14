using UnityEngine;

public class Component_GetCommericalBuildingConfig : MonoBehaviour, IComponent_GetCommercalBuildingConfig
{

    [SerializeField] private CommericalBuildingConfig _buildingConfig;

    public CommericalBuildingConfig GetBuildingConfig()
    {
        return _buildingConfig;
    }
}