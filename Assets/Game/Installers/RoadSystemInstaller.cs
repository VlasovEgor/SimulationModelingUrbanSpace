using UnityEngine;
using Zenject;

public class RoadSystemInstaller : MonoInstaller
{
    [SerializeField] private PlacementRoad _placementRoad;
    [SerializeField] private RoadFixer _roadFixer;
    [SerializeField] private RoadSystem _roadSystem;

    public override void InstallBindings()
    {
        BindPlacementRoad();
        BindRoadSystem();
        BindRoadFixer();
    }

    private void BindRoadSystem()
    {
        Container.BindInterfacesAndSelfTo<RoadSystem>().
            FromInstance(_roadSystem).
            AsSingle();
    }

    private void BindPlacementRoad()
    {
        Container.Bind<PlacementRoad>().
            FromInstance(_placementRoad).
            AsSingle();
    }

    private void BindRoadFixer()
    {
        Container.BindInterfacesAndSelfTo<RoadFixer>().
            FromInstance(_roadFixer).
            AsSingle();
    }
}
