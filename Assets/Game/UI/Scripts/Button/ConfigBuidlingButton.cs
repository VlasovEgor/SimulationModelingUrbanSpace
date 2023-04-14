
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ConfigBuidlingButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [Space]
    [SerializeField] private PopupName _currentPopup;
    [SerializeField] private PopupName _nextPopup;

    [Inject] private BuildingInfoShower _buildingInfoShower;

    private void Start()
    {
        _button.onClick.AddListener(SwitchBuildingConfigInfo);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SwitchBuildingConfigInfo);
    }

    private void SwitchBuildingConfigInfo()
    {
        _buildingInfoShower.HideBuildingInfo(_currentPopup);
        _buildingInfoShower.ShowBuildingInfo(_nextPopup);
    }
}
