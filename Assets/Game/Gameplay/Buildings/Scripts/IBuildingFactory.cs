
using Entities;
using UnityEngine;

public interface IBuildingFactory 
{
    GameObject Create(GameObject buildingPrefab,Transform parentTransform);
}
