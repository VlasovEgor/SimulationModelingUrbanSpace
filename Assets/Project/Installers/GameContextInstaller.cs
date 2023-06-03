using Zenject;

public class GameContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGraph();
        BindGraphSearch();
        BindPlacementManager();
        BindStructureCreator();
        BindStatisticsCollector();
    }

    private void BindGraph()
    {
        Container.Bind<Graph>().
            AsSingle();
    }

    private void BindGraphSearch()
    {
        Container.Bind<GraphSearch>().
            AsSingle();
    }

    private void BindPlacementManager()
    {
        Container.BindInterfacesAndSelfTo<PlacementManager>().
            AsSingle();
    }

    private void BindStructureCreator()
    {
        Container.Bind<StructureCreator>().
            AsSingle();
    }

    private void BindStatisticsCollector()
    {
        Container.BindInterfacesAndSelfTo<StatisticsCollector>().
            AsSingle();
    }
}
