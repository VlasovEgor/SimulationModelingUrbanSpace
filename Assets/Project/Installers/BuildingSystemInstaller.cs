using UnityEngine;
using Zenject;

public class BuildingSystemInstaller : MonoInstaller
{
    [SerializeField] private Transform _parentBuildingTransform;

    public override void InstallBindings()
    {
        BindBuildingFactory();
        BindBuildingsManager();
        BindBuildingSelector();
        BindBuildingCreator();
        BindBuildingDestroyer();
        BindBuildingInfoShower();
    }

    private void BindBuildingFactory()
    {
        Container.Bind<IBuildingFactory>().
            To<BuildingFactory>().
            AsSingle();
    }

    private void BindBuildingsManager()
    {
        Container.BindInterfacesAndSelfTo<BuildingsManager>().
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
            AsSingle();

        var buildingCreator = Container.Resolve<BuildingCreator>();
        buildingCreator.SetParentTransform(_parentBuildingTransform);
    }

    private void BindBuildingDestroyer()
    {
        Container.BindInterfacesAndSelfTo<BuildingDestroyer>().
            AsSingle();
    }

    private void BindBuildingInfoShower()
    {
        Container.BindInterfacesAndSelfTo<BuildingInfoShower>().
            AsSingle();
    }
}
