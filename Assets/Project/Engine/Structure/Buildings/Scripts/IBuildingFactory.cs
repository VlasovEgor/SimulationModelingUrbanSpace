
using Entities;
using UnityEngine;

public interface IBuildingFactory
{
    BuildingFactory BuildingFactory { get; set; }

    GameObject Create(GameObject buildingPrefab,Transform parentTransform);
}
