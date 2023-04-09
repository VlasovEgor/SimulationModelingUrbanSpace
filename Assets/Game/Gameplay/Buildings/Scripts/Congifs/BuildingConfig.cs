using Sirenix.OdinInspector;
using UnityEngine;

public class BuildingConfig : MonoBehaviour
{
   [SerializeField] private VertexType _vertexType;
   [ShowInInspector, ReadOnly] private Vector3 _nearestRoad;
   [ShowInInspector,ReadOnly] private Vector3 _position;
    
    public VertexType GetVertexType()
    {
        return _vertexType;
    }

    public void SetPosition(Vector3 position)
    {
        _position= position;
    }

    public Vector3 GetPosition()
    {
        return _position;
    }

    public void SetNearestRoad(Vector3 position)
    {
        _nearestRoad = position;
    }

    public Vector3 GetNearestRoad()
    {
        return _nearestRoad;
    }

}
