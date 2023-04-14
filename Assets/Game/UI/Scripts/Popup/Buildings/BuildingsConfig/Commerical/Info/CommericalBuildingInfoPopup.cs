using System;
using TMPro;
using UnityEngine;

public class CommericalBuildingInfoPopup : Popup
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _type;

    [Space]
    [SerializeField] private TextMeshProUGUI _numberEmployeesWithHigherEducation;
    [SerializeField] private TextMeshProUGUI _numberEmployeesWithSecondaryEducation;
    [SerializeField] private TextMeshProUGUI _numberEmployeesWithoutEducation;

    [Space]
    [SerializeField] private TextMeshProUGUI _numberVisitors;
    [SerializeField] private TextMeshProUGUI _amountOfSatisfactionOfNeed;

    [Space]
    [SerializeField] private TextMeshProUGUI _workTime;

    protected override void OnShow(object args)
    {
        if (args is not ICommericalBuildingConfigPresentationModel presenter)
        {
            throw new Exception("Expected Presentation model!");
        }

        _name.text = presenter.GetName();

        _type.text = presenter.GetStringBuildingType();

        _numberEmployeesWithHigherEducation.text = presenter.GetNumberEmployeesWithHigherEducation();
        _numberEmployeesWithSecondaryEducation.text = presenter.GetNumberEmployeesWithSecondaryEducation();
        _numberEmployeesWithoutEducation.text = presenter.GetNumberEmployeesWithoutEducation();

        _numberVisitors.text = presenter.GetMaximumNumberVisitors();
        _amountOfSatisfactionOfNeed.text = presenter.GetAmountOfSatisfactionOfNeed();

        var hourStartWork = presenter.GetHourStartWork();
        var minuteStartWork = presenter.GetMinuteStartWork();

        var hourFinishtWork = presenter.GetHourFinishWork();
        var minuteFinishWork = presenter.GetMinuteFinishWork();

        _workTime.text = "C " + hourStartWork + ":" + minuteStartWork + " До " + hourFinishtWork + ":" + minuteFinishWork;

    }
}
