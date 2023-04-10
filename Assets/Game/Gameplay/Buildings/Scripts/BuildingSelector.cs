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
    [Inject] private PlacementManager _placementManager;

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

        if (Physics.Raycast(ray, out RaycastHit raycastHit) == true)
        {
            if (raycastHit.collider != null)
            {
                _currentBulding = raycastHit.collider.GetComponent<UnityEntityProxy>();
                _graph.RemoveVertex(_graph.GetVertexByPosition(_currentBulding.transform.position));

                var buldingConfig = _currentBulding.Get<IComponent_GetBuildingConfig>().GetBuildingConfig();
                _placementManager.RemoveBuilding(buldingConfig);
            }
        }
    }

    private void PutUpBuilding()
    {
        if (CanPutUpBuilding() == false)
        {
            return;
        }

        var position = _currentBulding.Get<IComponent_PositionBuilding>().GetPosition();
        var building = _currentBulding.GameObject();
        var neighbour = _graph.SearchNearestVertex(position);
        var type = _currentBulding.Get<IComponent_GetVertexTypeBuilding>().GetVertexTypeBuilding();

        UrbanVertex newVertex = new(position, building, type);
        _graph.AddVertex(newVertex);
        _graph.AddEdge(newVertex, neighbour);

        var buldingConfig = _currentBulding.Get<IComponent_GetBuildingConfig>().GetBuildingConfig();
        buldingConfig.SetNearestRoad(GetNearestRoad(position));
        _placementManager.AddBuilding(buldingConfig);

        _currentBulding = null;
    }

    private bool CanPutUpBuilding()
    {
        var buildingPosition = _currentBulding.Get<IComponent_PositionBuilding>().GetPosition();
        if (_currentBulding != null &&
            _currentBulding.Get<IComponent_CanBuild>().CanBuild() == true &&
            Vector3.Distance(buildingPosition, _graph.SearchNearestVertex(buildingPosition).Position) <= _maximumDistance)
        {

            return true;
        }

        return false;
    }

    private Vector3 GetNearestRoad(Vector3 position)
    {
       var vertex = _graph.GetVertexByPosition(position);
       var neighbors = _graph.GetVerticesList(vertex);

        foreach (var neighbor in neighbors)
        {
            if(neighbor.VertexType == VertexType.Road)
            {
                return neighbor.Position;
            }
        }

        throw new Exception("there are no roads among the neighbors");

    }
}