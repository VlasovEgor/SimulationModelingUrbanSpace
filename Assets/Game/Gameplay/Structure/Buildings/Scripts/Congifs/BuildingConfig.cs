using Sirenix.OdinInspector;
using UnityEngine;

public class BuildingConfig : MonoBehaviour
{
    [Space]
    [SerializeField] protected BuidingType _buidingType;

    [SerializeField] private VertexType _vertexType;
    [SerializeField] private Transform _visualTransform;

    [ShowInInspector, ReadOnly] private Vector3 _nearestRoad;

    public VertexType GetVertexType()
    {
        return _vertexType;
    }

    public BuidingType GetBuidingType()
    {
        return _buidingType;
    }

    public void SetType(BuidingType buidingType)
    {
        _buidingType = buidingType;
    }

    public Vector3 GetPosition()
    {
        return _visualTransform.position;
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