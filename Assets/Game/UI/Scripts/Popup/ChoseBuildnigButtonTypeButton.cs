using UnityEngine;
using UnityEngine.UI;

public class ChoseBuildnigButtonTypeButton : MonoBehaviour
{
    [SerializeField] private PopupName _buildingPopup;
    [SerializeField] private BuildingsTypePopupManagerArgs buildingsTypePopup;

    [Space]
    [SerializeField] private Button _button;
    [SerializeField] private PopupManager _popupManager;
    [SerializeField] private ButtonChoseBuildingType _choseBuildingType;

    public void ChoosingType()
    {
        _choseBuildingType.RequestClose();
        _popupManager.ShowPopup(_buildingPopup, buildingsTypePopup);
    }
}
