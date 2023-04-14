using Sirenix.OdinInspector;
using Zenject;

public class ResidentialBuildingConfigShower
{
    [Inject] private PopupManager _popupManager;
    [Inject] private ResidentialBuildingConfigFactory _presenerFactory;

    [Button]
    public void ShowConfig(ResidentialBuildingConfig buildingConfig)
    {
        var presentationModel = _presenerFactory.CreatePresenter(buildingConfig);
        _popupManager.ShowPopup(PopupName.COMMERICAL_BUILDING_CONFIG, presentationModel);
    }
}
