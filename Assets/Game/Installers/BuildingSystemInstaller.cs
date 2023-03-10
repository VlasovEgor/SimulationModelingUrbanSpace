using UnityEngine;
using Zenject;

public class BuildingSystemInstaller : MonoInstaller
{
    [SerializeField] private BuildingCreator _buildingCreator;
    [SerializeField] private BuildingsManager _buildingsManager;

    public override void InstallBindings()
    {
        BindBuildingFactory();
        BindBuildingsManager();
        BindBuildingSelector();
        BindBuildingCreator();
    }

    private void BindBuildingFactory()
    {
        Container.Bind<IBuildingFactory>().
            To<BuildingFactory>().
            AsSingle();
    }

    private void BindBuildingsManager()
    {
        Container.Bind<BuildingsManager>().
            FromInstance(_buildingsManager).
            AsSingle();
    }

    private void BindBuildingSelector()
    {
        Container.BindInterfacesAndSelfTo<BuildingSelector>().
            AsSingle();
    }

    private void BindBuildingCreator()
    {
        Container.BindInterfacesAndSelfTo<BuildingCreator>().
            FromInstance(_buildingCreator).
            AsSingle();
    }
}
