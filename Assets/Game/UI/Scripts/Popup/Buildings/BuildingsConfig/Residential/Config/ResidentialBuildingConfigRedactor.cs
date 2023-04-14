
public class ResidentialBuildingConfigRedactor
{
    public void ChangeValuesInConfig(ResidentialBuildingConfig oldBuildingConfig, ResidentialBuildingConfig newBuildingConfig)
    {
        oldBuildingConfig.SetNumberResidentsWithHigherEducation(newBuildingConfig.GetNumberResidentsWithHigherEducation());
        oldBuildingConfig.SetNumberResidentsWithSecondaryEducation(newBuildingConfig.GetNumberResidentsWithSecondaryEducation());
        oldBuildingConfig.SetNumberResidentsWithoutEducation(newBuildingConfig.GetNumberResidentsWithoutEducation());
    }
}
