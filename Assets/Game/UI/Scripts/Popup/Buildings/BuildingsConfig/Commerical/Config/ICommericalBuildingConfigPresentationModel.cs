
using System.Collections.Generic;
using UnityEngine.UI;

public interface ICommericalBuildingConfigPresentationModel
{

    void OnAcceptButtonClicked(CommericalBuildingConfig newBuildingConfig);

    string GetName();

    int GetIndexBuildingType();

    string GetStringBuildingType();

    string GetNumberEmployeesWithHigherEducation();

    string GetNumberEmployeesWithSecondaryEducation();

    string GetNumberEmployeesWithoutEducation();

    string GetMaximumNumberVisitors();

    string GetAmountOfSatisfactionOfNeed();

    string GetHourStartWork();

    string GetMinuteStartWork();

    string GetHourFinishWork();

    string GetMinuteFinishWork();
    List<string> ListCommericalBuildingType();
}
