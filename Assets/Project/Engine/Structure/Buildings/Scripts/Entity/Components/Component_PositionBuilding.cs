using UnityEngine;

public class Component_PositionBuilding : MonoBehaviour, IComponent_PositionBuilding
{
    [SerializeField] private BuildingConfig _buildingConfig;

    public IComponent_PositionBuilding IComponent_PositionBuilding
    {
        get => default;
        set
        {
        }
    }

    public BuildingConfig BuildingConfig
    {
        get => default;
        set
        {
        }
    }

    public Vector3 GetPosition()
    {
        return _buildingConfig.GetPosition();
    }
}
