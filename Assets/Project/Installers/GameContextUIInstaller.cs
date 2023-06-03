using Zenject;

public class GameContextUIInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindCommericalBuildingConfigFactory();
        BindCommericalBuildingConfigShower();
        BindCommericalBuildingConfigRedactor();

        BindResidentialBuildingConfigFactory();
        BindResidentialBuildingConfigShower();
        BindResidentialBuildingConfigRedactor();

        BindClock();
    }

    private void BindCommericalBuildingConfigFactory()
    {
        Container.Bind<CommericalBuildingConfigFactory>().
            AsSingle();
    }

    private void BindCommericalBuildingConfigShower()
    {
        Container.Bind<CommericalBuildingConfigShower>().
            AsSingle();
    }

    private void BindCommericalBuildingConfigRedactor()
    {
        Container.BindInterfacesAndSelfTo<CommericalBuildingConfigRedactor>().
            AsSingle(); 
    }

    private void BindResidentialBuildingConfigFactory()
    {
        Container.Bind<ResidentialBuildingConfigFactory>().
            AsSingle();
    }

    private void BindResidentialBuildingConfigShower()
    {
        Container.Bind<ResidentialBuildingConfigShower>().
            AsSingle();
    }

    private void BindResidentialBuildingConfigRedactor()
    {
        Container.Bind<ResidentialBuildingConfigRedactor>().
            AsSingle();
    }

    private void BindClock()
    {
        Container.BindInterfacesAndSelfTo<Clock>().
            AsSingle();
    }
}
