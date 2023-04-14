
public class CommericalBuildingConfigRedactor
{
    public void ChangeValuesInConfig(CommericalBuildingConfig oldBuildingConfig, CommericalBuildingConfig newBuildingConfig)
    {
        oldBuildingConfig.SetName(newBuildingConfig.GetName());
        oldBuildingConfig.SetType(newBuildingConfig.GetCommericalBuidingType());

        oldBuildingConfig.SetMaximumNumberEmployeesWithHigherEducation(newBuildingConfig.GetMaximumNumberEmployeesWithHigherEducation());
        oldBuildingConfig.SetMaximumNumberEmployeesWithSecondaryEducation(newBuildingConfig.GetMaximumNumberEmployeesWithSecondaryEducation());
        oldBuildingConfig.SetMaximumNumberEmployeesWithoutEducation(newBuildingConfig.GetMaximumNumberEmployeesWithoutEducation());

        oldBuildingConfig.SetAmountOfSatisfactionOfNeed(newBuildingConfig.GetAmountOfSatisfactionOfNeed());
        oldBuildingConfig.SetMaximumNumberVisitors(newBuildingConfig.GetMaximumNumberVisitors());

        oldBuildingConfig.SetStartWork(newBuildingConfig.GetStartWork());
        oldBuildingConfig.SetFinishWork(newBuildingConfig.GetFinishWork());
    }
}
