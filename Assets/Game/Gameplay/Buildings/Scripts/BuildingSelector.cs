using Entities;
using System;
using UnityEngine;
using Zenject;

public class BuildingSelector : IInitializable, IDisposable
{
    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;
    [Inject] private BuildingCreator _buildingCreator;

    private UnityEntity _currentBulding;

    void IInitializable.Initialize()
    {
        _manipulationInput.LeftMouseButtonClicked += PutUpBuilding;
        _manipulationInput.LeftMouseButtonDoubleClicked += SelectBuilding;
        _buildingCreator.BuildingCreated += SetCurrentBuilding;
    }

    void IDisposable.Dispose()
    {
        _manipulationInput.LeftMouseButtonClicked -= PutUpBuilding;
        _manipulationInput.LeftMouseButtonDoubleClicked += SelectBuilding;
        _buildingCreator.BuildingCreated -= SetCurrentBuilding;
    }

    public void SetCurrentBuilding(UnityEntity currentBuilding)
    {
        _currentBulding = currentBuilding;
    }

    public UnityEntity GetCurrentBuilding()
    {
        return _currentBulding;
    }

    private void SelectBuilding()
    {   
        if(_currentBulding != null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit) == true)
        {
            if(raycastHit.collider != null)
            {
                _currentBulding = raycastHit.collider.GetComponent<UnityEntityProxy>();
            }
        }
    }

    private void PutUpBuilding()
    {  
        if(_currentBulding != null && _currentBulding.Get<ICanBuild_Component>().CanBuild() == true)
        {
            _currentBulding = null;
        }
    }
}