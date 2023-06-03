using UnityEngine;
using UnityEngine.UI;

public class SwitchTypeBuildingButton : MonoBehaviour
{
    [SerializeField] private BuildingsTypePopupManagerArgs buildingsTypePopup;

    [Space]
    [SerializeField] private Button _button;
    [SerializeField] private BuildingListPresenter _buildingListPresenter;

    public BuildingListPresenter BuildingListPresenter
    {
        get => default;
        set
        {
        }
    }

    public void SwitchingType()
    {
        _buildingListPresenter.Hide();
        _buildingListPresenter.Show(buildingsTypePopup.ToString());
    }

}