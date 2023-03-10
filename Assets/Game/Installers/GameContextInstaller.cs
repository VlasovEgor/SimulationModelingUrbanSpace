using Zenject;

public class GameContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMoveInDirectionController();
        BindRotateController();
        BindZoomController();
        BindBoostController();
    }

    private void BindMoveInDirectionController()
    {
        Container.BindInterfacesAndSelfTo<MoveInDirectionController>().
            AsSingle();
    }

    private void BindRotateController()
    {
        Container.BindInterfacesAndSelfTo<RotateController>().
            AsSingle();
    }

    private void BindZoomController()
    {
        Container.BindInterfacesAndSelfTo<ZoomController>().
            AsSingle();
    }

    private void BindBoostController()
    {
        Container.BindInterfacesAndSelfTo<BoostController>().
            AsSingle();
    }
}
