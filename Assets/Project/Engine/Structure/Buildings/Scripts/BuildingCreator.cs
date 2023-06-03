using Entities;
using System;
using UnityEngine;
using Zenject;

public class BuildingCreator
{
    public event Action<UnityEntity> BuildingCreated;

    public IBuildingFactory IBuildingFactory
    {
        get => default;
        set
        {
        }
    }

    [SerializeField] private Transform _parentTransform;

    [Inject] private IBuildingFactory _buildingFactory;

    public void SetParentTransform(Transform transform)
    {
        _parentTransform = transform;
    }

    public void Create(GameObject buildingPrefab)
    {
      var currentBuilding = _buildingFactory.Create(buildingPrefab, _parentTransform);
      BuildingCreated?.Invoke(currentBuilding.GetComponent<UnityEntity>());
    }
}
