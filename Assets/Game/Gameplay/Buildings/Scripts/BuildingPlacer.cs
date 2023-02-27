using Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class BuildingPlacer : MonoBehaviour 
{
    [ShowInInspector, ReadOnly] private UnityEntity _currentBulding;

    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;

    private Plane _plane;

    private void Start()
    {
        _plane = new Plane(Vector3.up, Vector3.zero);
        _manipulationInput.LeftMouseButtonClicked += PutUpBuilding;
        _manipulationInput.LeftMouseButtonDoubleClicked+= SelectBuilding;
        _manipulationInput.RotatedKeyboard += RotateBuilding;
    }

    private void Update()
    {
        MoveBuilding();
    }

    private void MoveBuilding()
    {
        if (_currentBulding == null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);

        Vector3 point = ray.GetPoint(distance);

        _currentBulding.Get<IComponent_MoveBuilding>().MoveBuilding(point);
    }

    private void SelectBuilding()
    {   
        if(_currentBulding != null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if(raycastHit.collider != null)
            {
                _currentBulding= raycastHit.collider.GetComponent<UnityEntityProxy>().GetBaseEntity();
            }
        }
    }

    private void PutUpBuilding()
    {  
        if(_currentBulding != null)
        {
            _currentBulding = null;
        }
    }

    private void RotateBuilding(int direction)
    {
        if (_currentBulding != null)
        {
            _currentBulding.GetComponentInChildren<Component_BuildingRotate>().Rotate(direction);
        }
    }

    private void OnDestroy()
    {
        _manipulationInput.LeftMouseButtonClicked -= PutUpBuilding;
        _manipulationInput.RotatedKeyboard -= RotateBuilding;
        _manipulationInput.LeftMouseButtonDoubleClicked += SelectBuilding;
    }

    
}
