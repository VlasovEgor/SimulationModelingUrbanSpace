using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CommericalBuildingConfigPopup : Popup
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private TMP_Dropdown _typeDropdown;

    [Space]
    [SerializeField] private TMP_InputField _numberEmployeesWithHigherEducation;
    [SerializeField] private TMP_InputField _numberEmployeesWithSecondaryEducation;
    [SerializeField] private TMP_InputField _numberEmployeesWithoutEducation;

    [Space]
    [SerializeField] private TMP_InputField _maximumNumberVisitors;
    [SerializeField] private TMP_InputField _amountOfSatisfactionOfNeed;

    [Space]
    [SerializeField] private TMP_InputField _averageTimeInBuilding;


    [Space]
    [SerializeField] private TMP_InputField _hourStartWork;
    [SerializeField] private TMP_InputField _minuteStartWork;

    [Space]
    [SerializeField] private TMP_InputField _hourFinishtWork;
    [SerializeField] private TMP_InputField _minuteFinishWork;

    [Space]
    [SerializeField] private Button _acceptButton;

    private ICommericalBuildingConfigPresentationModel _presenter;

    public ICommericalBuildingConfigPresentationModel ICommericalBuildingConfigPresentationModel
    {
        get => default;
        set
        {
        }
    }

    protected override void OnShow(object args)
    {
        if (args is not ICommericalBuildingConfigPresentationModel presenter)
        {
            throw new Exception("Expected Presentation model!");
        }

        _presenter = presenter;

        _nameInputField.text = presenter.GetName();

        _typeDropdown.ClearOptions();
        _typeDropdown.AddOptions(presenter.ListCommericalBuildingType());


        _numberEmployeesWithHigherEducation.text = presenter.GetMaximumNumberEmployeesWithHigherEducation();
        _numberEmployeesWithSecondaryEducation.text = presenter.GetMaximumNumberEmployeesWithSecondaryEducation();
        _numberEmployeesWithoutEducation.text = presenter.GetMaximumNumberEmployeesWithoutEducation();

        _maximumNumberVisitors.text = presenter.GetMaximumNumberVisitors();
        _amountOfSatisfactionOfNeed.text = presenter.GetAmountOfSatisfactionOfNeed();
        _averageTimeInBuilding.text = presenter.GetAverageTimeInBuilding();

        _hourStartWork.text = presenter.GetHourStartWork();
        _minuteStartWork.text = presenter.GetMinuteStartWork();

        _hourFinishtWork.text = presenter.GetHourFinishWork();
        _minuteFinishWork.text = presenter.GetMinuteFinishWork();

        _acceptButton.onClick.AddListener(OnAcceptButtonClicked);
    }

    protected override void OnHide()
    {
        _acceptButton.onClick.RemoveListener(OnAcceptButtonClicked);
    }

    private void OnAcceptButtonClicked()
    {
        var newData = CreateNewDataConfig();
        _presenter.OnAcceptButtonClicked(newData);
    }

    private CommericalBuildingConfig CreateNewDataConfig()
    {
        CommericalBuildingConfig newData = new();

        newData.SetName(_nameInputField.text);
        newData.SetType(_typeDropdown.value+2);

        newData.SetMaximumNumberEmployeesOfCertainEducation(Education.HIGHER_EDUCATION,int.Parse(_numberEmployeesWithHigherEducation.text));
        newData.SetMaximumNumberEmployeesOfCertainEducation(Education.SECOND_EDUCATION, int.Parse(_numberEmployeesWithSecondaryEducation.text));
        newData.SetMaximumNumberEmployeesOfCertainEducation(Education.WITOUT_EDUCATION, int.Parse(_numberEmployeesWithoutEducation.text));

        newData.SetMaximumNumberVisitors(int.Parse(_maximumNumberVisitors.text));
        newData.SetAmountOfSatisfactionOfNeed(int.Parse(_amountOfSatisfactionOfNeed.text));

        newData.SetAverageTimeInBuilding(int.Parse(_averageTimeInBuilding.text));

        HourMinute hourMinute = new();
        hourMinute.Hour = int.Parse(_hourStartWork.text);
        hourMinute.Minute= int.Parse(_minuteStartWork.text);
        newData.SetStartWork(hourMinute);

        hourMinute.Hour = int.Parse(_hourFinishtWork.text);
        hourMinute.Minute = int.Parse(_minuteFinishWork.text);
        newData.SetFinishWork(hourMinute);

        return newData;
    }
}