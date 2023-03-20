using System;
using UnityEngine;

public class BuildingSelectionPopup : Popup
{
    [SerializeField] private BuildingListPresenter _buildingListPresenter;
    [SerializeField] private ButtonManager _buttonManager;

    protected override void OnShow(object args)
    {
        if (args is not BuildingsTypePopupManagerArgs buildingsType)
        {
            throw new Exception("Expected BuildingsType!");
        }

        _buildingListPresenter.Show(buildingsType.ToString());
    }

    protected override void OnHide()
    {   
        _buildingListPresenter.Hide();
        _buttonManager.ShowButton(ButtonName.CHOSE_BUILDING_TYPE);
    }
}
