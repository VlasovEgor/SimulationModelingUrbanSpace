
using UnityEngine;

public class ResidentialBuildingConfigPresentationModel : IResidentialBuildingConfigPresentationModel
{   
    private ResidentialBuildingConfig _buildingConfig;
    private ResidentialBuildingConfigRedactor _configRedactor;

    public ResidentialBuildingConfigPresentationModel(ResidentialBuildingConfig buildingConfig, ResidentialBuildingConfigRedactor configRedactor)
    {
        _buildingConfig = buildingConfig;
        _configRedactor = configRedactor;
    }

    public ResidentialBuildingConfigRedactor ResidentialBuildingConfigRedactor
    {
        get => default;
        set
        {
        }
    }

    void IResidentialBuildingConfigPresentationModel.OnAcceptButtonClicked(ResidentialBuildingConfig newBuildingConfig)
    {
        _configRedactor.ChangeValuesInConfig(_buildingConfig, newBuildingConfig);
        _configRedactor.ChangeNumberCitizens(_buildingConfig);
    }

    public string GetNumberEmployeesWithHigherEducation()
    {
        return _buildingConfig.GetNumberResidentsWithHigherEducation().ToString();
    }

    public string GetNumberEmployeesWithSecondaryEducation()
    {
        return _buildingConfig.GetNumberResidentsWithSecondaryEducation().ToString();
    }

    public string GetNumberEmployeesWithoutEducation()
    {
        return _buildingConfig.GetNumberResidentsWithoutEducation().ToString();
    }
}