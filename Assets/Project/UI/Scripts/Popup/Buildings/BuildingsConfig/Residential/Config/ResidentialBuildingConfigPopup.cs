using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResidentialBuildingConfigPopup : Popup
{
    [Space]
    [SerializeField] private TMP_InputField _numberResidentsWithHigherEducation;
    [SerializeField] private TMP_InputField _numberResidentsWithSecondaryEducation;
    [SerializeField] private TMP_InputField _numberResidentsWithoutEducation;

    [Space]
    [SerializeField] private Button _acceptButton;

    private IResidentialBuildingConfigPresentationModel _presenter;

    public IResidentialBuildingConfigPresentationModel IResidentialBuildingConfigPresentationModel
    {
        get => default;
        set
        {
        }
    }

    protected override void OnShow(object args)
    {
        if (args is not IResidentialBuildingConfigPresentationModel presenter)
        {
            throw new Exception("Expected Presentation model!");
        }

        _presenter = presenter;

        _numberResidentsWithHigherEducation.text = presenter.GetNumberEmployeesWithHigherEducation();
        _numberResidentsWithSecondaryEducation.text = presenter.GetNumberEmployeesWithSecondaryEducation();
        _numberResidentsWithoutEducation.text = presenter.GetNumberEmployeesWithoutEducation();

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

    private ResidentialBuildingConfig CreateNewDataConfig()
    {
        ResidentialBuildingConfig newData = new();

        newData.SetNumberResidentsWithHigherEducation(int.Parse(_numberResidentsWithHigherEducation.text));
        newData.SetNumberResidentsWithSecondaryEducation(int.Parse(_numberResidentsWithSecondaryEducation.text));
        newData.SetNumberResidentsWithoutEducation(int.Parse(_numberResidentsWithoutEducation.text));

        return newData;
    }
}