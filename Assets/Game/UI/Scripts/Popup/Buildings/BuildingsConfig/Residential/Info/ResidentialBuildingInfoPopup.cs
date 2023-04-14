using System;
using TMPro;
using UnityEngine;

public class ResidentialBuildingInfoPopup : Popup
{
    [Space]
    [SerializeField] private TextMeshProUGUI _numberResidentsWithHigherEducation;
    [SerializeField] private TextMeshProUGUI _numberResidentsWithSecondaryEducation;
    [SerializeField] private TextMeshProUGUI _numberResidentsWithoutEducation;

    protected override void OnShow(object args)
    {
        if (args is not IResidentialBuildingConfigPresentationModel presenter)
        {
            throw new Exception("Expected Presentation model!");
        }

        _numberResidentsWithHigherEducation.text = presenter.GetNumberEmployeesWithHigherEducation();
        _numberResidentsWithSecondaryEducation.text = presenter.GetNumberEmployeesWithSecondaryEducation();
        _numberResidentsWithoutEducation.text = presenter.GetNumberEmployeesWithoutEducation();

    }
}
