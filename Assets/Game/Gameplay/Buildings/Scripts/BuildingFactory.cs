using UnityEngine;
using Zenject;

public class BuildingFactory : IBuildingFactory
{   
    private readonly DiContainer _container;

    public BuildingFactory(DiContainer diContainer)
    {
        _container = diContainer;
    }

    public GameObject Create(GameObject buildingPrefab)
    {
        return  _container.InstantiatePrefab(buildingPrefab);
    }

}
