using Entities;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BuildingSelector : Zenject.IInitializable, IDisposable
{
    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;
    [Inject] private BuildingCreator _buildingCreator;
    [Inject] private Graph _graph;

    private UnityEntity _currentBulding;
    private float _maximumDistance = 30f;

    void Zenject.IInitializable.Initialize()
    {
        _manipulationInput.LeftMouseButtonDown += PutUpBuilding;
        _manipulationInput.LeftMouseButtonDoubleClicked += SelectBuilding;
        _buildingCreator.BuildingCreated += SetCurrentBuilding;
    }

    void IDisposable.Dispose()
    {
        _manipulationInput.LeftMouseButtonDown -= PutUpBuilding;
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
        if (_currentBulding != null)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit) == true)
        {
            if (raycastHit.collider != null)
            {
                _currentBulding = raycastHit.collider.GetComponent<UnityEntityProxy>();
                _graph.RemoveVertex(_graph.GetVertexByPosition(_currentBulding.transform.position));
            }
        }
    }

    private void PutUpBuilding()
    {
        if (CanPutUpBuilding() == false)
        {
            return;
        }

        var position = _currentBulding.transform.position;
        var building = _currentBulding.GameObject();
        var neighbour = _graph.SearchNearestVertex(position);

        Vertex newVertex = new(position, building, VertexType.Residential_Building);
        _graph.AddVertex(newVertex);
        _graph.AddEdge(newVertex, neighbour);

        _currentBulding = null;
    }

    private bool CanPutUpBuilding()
    {
        if (_currentBulding != null &&
            _currentBulding.Get<ICanBuild_Component>().CanBuild() == true &&
            Vector3.Distance(_currentBulding.transform.position, _graph.SearchNearestVertex(_currentBulding.transform.position).Position) <= _maximumDistance)
        {

            return true;
        }

        return false;
    }
}