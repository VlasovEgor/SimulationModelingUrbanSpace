using Zenject;

public class CommericalBuildingConfigFactory
{
    [Inject] private CommericalBuildingConfigRedactor _configRedactor;

    public CommericalBuildingConfigPresentationModel CreatePresenter(CommericalBuildingConfig buildingConfig)
    {
        return new CommericalBuildingConfigPresentationModel(buildingConfig, _configRedactor);
    }
}
