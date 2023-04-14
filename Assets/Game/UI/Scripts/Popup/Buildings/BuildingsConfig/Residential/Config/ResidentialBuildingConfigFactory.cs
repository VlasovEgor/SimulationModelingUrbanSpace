using Zenject;

public class ResidentialBuildingConfigFactory
{
    [Inject] private ResidentialBuildingConfigRedactor _configRedactor;

    public ResidentialBuildingConfigPresentationModel CreatePresenter(ResidentialBuildingConfig buildingConfig)
    {
        return new ResidentialBuildingConfigPresentationModel(buildingConfig, _configRedactor);
    }
}
