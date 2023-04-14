using Entities;
using System;
using UnityEngine;
using Zenject;

public class BuildingInfoShower: IInitializable, IDisposable
{
    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;
    [Inject] private PopupManager _popupManager;
    [Inject] private CommericalBuildingConfigFactory _commericalConfigFactory;
    [Inject] private ResidentialBuildingConfigFactory _residentialConfigFactory;

    private UnityEntityProxy _currentBuilding;

    public void Initialize()
    {
        _manipulationInput.LeftMouseButtonUp += GettingBuilding;
    }

    public void Dispose()
    {
        _manipulationInput.LeftMouseButtonUp -= GettingBuilding;
    }

    private void GettingBuilding()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit) == true)
        {
            if (raycastHit.collider != null)
            {
                 _currentBuilding = raycastHit.collider.GetComponent<UnityEntityProxy>();

                if (_currentBuilding.Get<IComponent_GetVertexTypeBuilding>().GetVertexTypeBuilding() == VertexType.Commercial_Building)
                {
                    ShowBuildingInfo(PopupName.COMMERICAL_BUILDING_INFO);
                }
                else
                {
                    ShowBuildingInfo(PopupName.RESIDENTIAL_BUILDING_INFO);
                }
            }
        }
    }

    public void ShowBuildingInfo(PopupName popupName)
    {
        if (_currentBuilding.Get<IComponent_GetVertexTypeBuilding>().GetVertexTypeBuilding() == VertexType.Commercial_Building)
        {
            var buldingConfig = _currentBuilding.Get<IComponent_GetCommercalBuildingConfig>().GetBuildingConfig();
            var presentationModel = _commericalConfigFactory.CreatePresenter(buldingConfig);

            _popupManager.ShowPopup(popupName, presentationModel);
        }
        else
        {
            var buldingConfig = _currentBuilding.Get<IComponent_GetResidentialBuildingConfig>().GetBuildingConfig();
            var presentationModel = _residentialConfigFactory.CreatePresenter(buldingConfig);

            _popupManager.ShowPopup(popupName, presentationModel);
        }
    }

    public void HideBuildingInfo(PopupName popupName)
    {
        _popupManager.HidePopup(popupName);
    }
}
