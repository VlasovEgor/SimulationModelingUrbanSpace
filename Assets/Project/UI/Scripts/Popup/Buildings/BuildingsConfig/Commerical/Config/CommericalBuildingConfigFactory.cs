using Zenject;

public class CommericalBuildingConfigFactory
{
    [Inject] private CommericalBuildingConfigRedactor _configRedactor;

    public CommericalBuildingConfigRedactor CommericalBuildingConfigRedactor
    {
        get => default;
        set
        {
        }
    }

    public CommericalBuildingConfigPresentationModel CommericalBuildingConfigPresentationModel
    {
        get => default;
        set
        {
        }
    }

    public CommericalBuildingConfigPresentationModel CreatePresenter(CommericalBuildingConfig buildingConfig)
    {
        return new CommericalBuildingConfigPresentationModel(buildingConfig, _configRedactor);
    }
}
