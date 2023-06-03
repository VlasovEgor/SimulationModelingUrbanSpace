using System;
using UnityEngine;
using Zenject;

public class RoadSystem :MonoBehaviour, IInitializable, IDisposable
{
    public event Action RoadPaved;

    public PlacementRoad PlacementRoad
    {
        get => default;
        set
        {
        }
    }

    [Inject] private PlacementRoad _placementRoad;
    [Inject] private ManipulationInput _manipulationInput;
    [Inject] private Camera _camera;

    private GameObject _segmentRoad;

    private int _counter = -1;

    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private Plane _plane;

    private bool _canBuildRoad = false;

    public void Initialize()
    {
       // _manipulationInput.LeftMouseButtonUp += FindingMousePosition;
       // _manipulationInput.RightMouseButtonClicked += DisableAbilityBuildRoad;

        _plane = new Plane(Vector3.up, Vector3.zero);
    }

    public void Dispose()
    {
        _manipulationInput.LeftMouseButtonUp -= FindingMousePosition;
        _manipulationInput.RightMouseButtonClicked -= DisableAbilityBuildRoad;
    }

    private void FindingMousePosition()
    {
        if (_canBuildRoad == false)
        {
            return;
        }

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        _plane.Raycast(ray, out float distance);

        Vector3 point = ray.GetPoint(distance);


        if (_counter == -1)//crutch
        {
            _counter++;
            return;
        }
        if (_counter == 0)
        {
            _startPosition = point;
            _counter++;
        }
        else
        {
            _endPosition = point;
            PlaceRoad(_startPosition,_endPosition);
            _counter = 0;
        }
    }

    public void EnableAbilityBuildRoad()
    {
        _canBuildRoad = true;

        _manipulationInput.LeftMouseButtonUp += FindingMousePosition;
        _manipulationInput.RightMouseButtonClicked += DisableAbilityBuildRoad;
    }

    private void DisableAbilityBuildRoad()
    {   
        _canBuildRoad = false;

        _counter = -1;
        _startPosition = Vector3.zero;
        _endPosition = Vector3.zero;

        _manipulationInput.LeftMouseButtonUp -= FindingMousePosition;
        _manipulationInput.RightMouseButtonClicked -= DisableAbilityBuildRoad;
    }

    public void SetPavedRoad(GameObject segmentRoad)
    {
        _segmentRoad = segmentRoad;
    }
    
    private void PlaceRoad(Vector3 startPosition, Vector3 endPosition)
    {
        if(CheckIfPositionIsFree(startPosition, endPosition) == false)
        {
            return;
        }
        if(_canBuildRoad == false)
        {
            return;
        }

        _placementRoad.PaveRoad(startPosition, endPosition, _segmentRoad);
        RoadPaved?.Invoke();
    }

    private bool CheckIfPositionIsFree(Vector3 startPosition, Vector3 endPosition)
    {
        var startPoint = _placementRoad.GetNearestPosition(startPosition);
        var endPoint = _placementRoad.GetNearestPosition(endPosition);

        var direction = endPoint - startPoint;
        var lenght = Vector3.Distance(startPoint, endPoint);
        RaycastHit[] hits = Physics.RaycastAll(startPoint, direction, lenght);

        if (hits.Length == 0)
        {
            return true;
        }
        else if (hits.Length == 1)
        {
           if (Vector3.Distance(hits[0].transform.position, startPoint) < 10 || Vector3.Distance(hits[0].transform.position, endPoint) < 10)
            {
               return true;
           }
        }
        return false;
    }
}