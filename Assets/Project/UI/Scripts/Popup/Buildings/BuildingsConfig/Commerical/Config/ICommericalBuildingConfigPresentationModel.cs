
using System.Collections.Generic;

public interface ICommericalBuildingConfigPresentationModel
{

    void OnAcceptButtonClicked(CommericalBuildingConfig newBuildingConfig);

    string GetName();

    int GetIndexBuildingType();

    string GetStringBuildingType();

    string GetCurrentNumberEmployeesWithHigherEducation();

    string GetCurrentNumberEmployeesWithSecondaryEducation();

    string GetCurrentNumberEmployeesWithoutEducation();

    string GetMaximumNumberEmployeesWithHigherEducation();

    string GetMaximumNumberEmployeesWithSecondaryEducation();

    string GetMaximumNumberEmployeesWithoutEducation();

    string GetMaximumNumberVisitors();

    string GetAmountOfSatisfactionOfNeed();

    string GetHourStartWork();

    string GetMinuteStartWork();

    string GetHourFinishWork();

    string GetMinuteFinishWork();
    List<string> ListCommericalBuildingType();
    string GetAverageTimeInBuilding();
}
