using Zenject;

public class GameContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindGraph();
        BindStructureCreator();
    }

    private void BindGraph()
    {
        Container.Bind<Graph>().
            AsSingle();
    }

    private void BindStructureCreator()
    {
        Container.Bind<StructureCreator>().
            AsSingle();
    }
}
