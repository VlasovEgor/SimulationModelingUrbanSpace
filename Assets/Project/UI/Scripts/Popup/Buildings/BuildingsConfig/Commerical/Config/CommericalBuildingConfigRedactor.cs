using System;
using Zenject;

public class CommericalBuildingConfigRedactor: IInitializable, IDisposable
{
    public event Action<BuidingType> BuildingRedacted;
    public event Action<CommericalBuildingConfig> BuildingRedactedForPlacmentManager;

    [Inject] private PlacementManager _placementManager;
    [Inject] private CitizensManager _citizensManager;

    public void Initialize()
    {
        _placementManager.OnEmpolyeeAdded += AddEmployeeOfCertainEducation;
        _citizensManager.CitizenRemoved += RemoveEmployeeOfCertainEducation;
    }

    public void Dispose()
    {
        _placementManager.OnEmpolyeeAdded -= AddEmployeeOfCertainEducation;
        _citizensManager.CitizenRemoved -= RemoveEmployeeOfCertainEducation;
    }

    public void ChangeValuesInConfig(CommericalBuildingConfig oldBuildingConfig, CommericalBuildingConfig newBuildingConfig)
    {
        oldBuildingConfig.SetName(newBuildingConfig.GetName());
        oldBuildingConfig.SetType(newBuildingConfig.GetBuidingType());

        oldBuildingConfig.SetMaximumNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION, 
            newBuildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION));

        oldBuildingConfig.SetMaximumNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION, 
            newBuildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION));

        oldBuildingConfig.SetMaximumNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION, 
            newBuildingConfig.GetMaximumNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION));

        oldBuildingConfig.SetAmountOfSatisfactionOfNeed(newBuildingConfig.GetAmountOfSatisfactionOfNeed());
        oldBuildingConfig.SetMaximumNumberVisitors(newBuildingConfig.GetMaximumNumberVisitors());
        oldBuildingConfig.SetAverageTimeInBuilding(newBuildingConfig.GetAverageTimeInBuilding());

        oldBuildingConfig.SetStartWork(newBuildingConfig.GetStartWork());
        oldBuildingConfig.SetFinishWork(newBuildingConfig.GetFinishWork());

        BuildingRedacted?.Invoke(oldBuildingConfig.GetBuidingType());
        BuildingRedactedForPlacmentManager?.Invoke(oldBuildingConfig);
    }

    public void AddEmployeeOfCertainEducation(CommericalBuildingConfig buildingConfig, Education education )
    {
        buildingConfig.AddEmployeeOfCertainEducation(education);
    }

    public void RemoveEmployeeOfCertainEducation(Citizen citizen)
    {
        var commericalBuildingList = _placementManager.GetCommericalBuildingConfigList();

        foreach (var buildingConfig in commericalBuildingList)
        {
            if (citizen.GetPlaceActivity(BuidingType.WORK) == buildingConfig)
            {
                buildingConfig.RemoveEmployeeOfCertainEducation(citizen.GetEducation());
            }
        }
    }
}