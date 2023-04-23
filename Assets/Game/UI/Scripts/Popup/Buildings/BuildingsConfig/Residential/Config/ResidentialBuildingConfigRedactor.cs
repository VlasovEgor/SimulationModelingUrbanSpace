
using System;
using UnityEngine;

public class ResidentialBuildingConfigRedactor
{
    public event Action<Vector3,int, int, int> NumberResidentsChanged;

    public void ChangeValuesInConfig(ResidentialBuildingConfig oldBuildingConfig, ResidentialBuildingConfig newBuildingConfig)
    {
        oldBuildingConfig.SetNumberResidentsWithHigherEducation(newBuildingConfig.GetNumberResidentsWithHigherEducation());
        oldBuildingConfig.SetNumberResidentsWithSecondaryEducation(newBuildingConfig.GetNumberResidentsWithSecondaryEducation());
        oldBuildingConfig.SetNumberResidentsWithoutEducation(newBuildingConfig.GetNumberResidentsWithoutEducation());
    }

    public void ChangeNumberCitizens(ResidentialBuildingConfig buildingConfig)
    {
        NumberResidentsChanged?.Invoke(buildingConfig.GetPosition(), 
            buildingConfig.GetNumberResidentsWithHigherEducation(),
            buildingConfig.GetNumberResidentsWithSecondaryEducation(),
            buildingConfig.GetNumberResidentsWithoutEducation());
    }
}
