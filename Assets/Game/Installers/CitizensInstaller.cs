using Zenject;

public class CitizensInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCitizenManager();
        BindCitizenFactory();
    }

    private void BindCitizenManager()
    {
        Container.BindInterfacesAndSelfTo<CitizensManager>().
            AsSingle();
    }

    private void BindCitizenFactory()
    {
        Container.Bind<CitizenFactory>().
            AsSingle();
    }
}
