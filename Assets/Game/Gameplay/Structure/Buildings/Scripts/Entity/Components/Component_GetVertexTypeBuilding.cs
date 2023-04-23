using UnityEngine;

public class Component_GetVertexTypeBuilding : MonoBehaviour, IComponent_GetVertexTypeBuilding
{
    [SerializeField] private BuildingConfig _buildingConfig;

    public VertexType GetVertexTypeBuilding()
    {
        return _buildingConfig.GetVertexType();
    }
}
