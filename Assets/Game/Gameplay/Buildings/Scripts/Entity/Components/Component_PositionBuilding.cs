using UnityEngine;

public class Component_PositionBuilding : MonoBehaviour, IComponent_PositionBuilding
{
    [SerializeField] private BuildingConfig _buildingConfig;

    public Vector3 GetPosition()
    {
        return _buildingConfig.GetPosition();
    }

    public void SetPosition(Vector3 position)
    {
        _buildingConfig.SetPosition(position);
    }

   
}
