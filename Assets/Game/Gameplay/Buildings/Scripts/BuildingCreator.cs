using Entities;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

public class BuildingCreator : MonoBehaviour
{
    public event Action<UnityEntity> BuildingCreated;

    [Inject] private IBuildingFactory _buildingFactory;

    [Button]
    public void Create(GameObject buildingPrefab)
    {
      var currentBuilding = _buildingFactory.Create(buildingPrefab);
      BuildingCreated?.Invoke(currentBuilding.GetComponent<UnityEntity>());
    }
}
