using UnityEngine;

public class Component_GetVertexTypeBuilding : MonoBehaviour, IComponent_GetVertexTypeBuilding
{
    [SerializeField] private BuildingConfig _buildingConfig;

    public IComponent_GetVertexTypeBuilding IComponent_GetVertexTypeBuilding
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

    public VertexType GetVertexTypeBuilding()
    {
        return _buildingConfig.GetVertexType();
    }
}
