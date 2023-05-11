using Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class BuildingMover : MonoBehaviour
{
    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;
    [Inject] private BuildingSelector _buildingSelector;

    [ShowInInspector, ReadOnly] private UnityEntity _currentBulding;

    private Plane _plane;

    private void OnEnable()
    {
        _manipulationInput.RotatedKeyboard += RotateBuilding;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    private void OnDisable()
    {
        _manipulationInput.RotatedKeyboard -= RotateBuilding;
    }

    private void Update()
    {
        SetCurrentBuilding();
        MoveBuilding();
    }

    private void SetCurrentBuilding()
    {
        _currentBulding = _buildingSelector.GetCurrentBuilding();
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

        var correctPosition = new Vector3(point.x, point.y + 0.5f, point.z);
        _currentBulding.Get<IComponent_MoveBuilding>().MoveBuilding(correctPosition);
    }

    private void RotateBuilding(int direction)
    {
        if (_currentBulding != null)
        {
            _currentBulding.Get<IComponent_BuildingRotate>().Rotate(direction);
        }
    }
}
