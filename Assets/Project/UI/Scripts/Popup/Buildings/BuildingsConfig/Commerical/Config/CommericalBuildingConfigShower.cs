using Sirenix.OdinInspector;
using Zenject;

public class CommericalBuildingConfigShower
{
    [Inject] private PopupManager _popupManager;
    [Inject] private CommericalBuildingConfigFactory _presenerFactory;

    public PopupManager PopupManager
    {
        get => default;
        set
        {
        }
    }

    public CommericalBuildingConfigFactory CommericalBuildingConfigFactory
    {
        get => default;
        set
        {
        }
    }

    [Button]
    public void ShowConfig(CommericalBuildingConfig buildingConfig)
    {
        var presentationModel = _presenerFactory.CreatePresenter(buildingConfig);
        _popupManager.ShowPopup(PopupName.COMMERICAL_BUILDING_CONFIG, presentationModel);
    }
}
