using System.Collections.Generic;

public interface IResidentialBuildingConfigPresentationModel
{
    void OnAcceptButtonClicked(ResidentialBuildingConfig newBuildingConfig);

    string GetNumberEmployeesWithHigherEducation();

    string GetNumberEmployeesWithSecondaryEducation();

    string GetNumberEmployeesWithoutEducation();
}
