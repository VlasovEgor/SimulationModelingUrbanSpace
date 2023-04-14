using System;
using System.Collections.Generic;
using System.Linq;

public class ResidentialBuildingConfigPresentationModel : IResidentialBuildingConfigPresentationModel
{   
    private ResidentialBuildingConfig _buildingConfig;
    private ResidentialBuildingConfigRedactor _configRedactor;
    private List<string> _buildingType;

    public ResidentialBuildingConfigPresentationModel(ResidentialBuildingConfig buildingConfig, ResidentialBuildingConfigRedactor configRedactor)
    {
        _buildingConfig = buildingConfig;
        _configRedactor = configRedactor;
    }

    void IResidentialBuildingConfigPresentationModel.OnAcceptButtonClicked(ResidentialBuildingConfig newBuildingConfig)
    {
        _configRedactor.ChangeValuesInConfig(_buildingConfig, newBuildingConfig);
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