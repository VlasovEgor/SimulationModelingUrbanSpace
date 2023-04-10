using System;
using System.Collections.Generic;
using UnityEngine;

public class RoadHelper : MonoBehaviour
{
    [SerializeField]
    private List<Marker> _pedestrianMarkers;
    [SerializeField]
    private List<Marker> _carMarkers;
    [SerializeField]
    private bool _isCorner;
    [SerializeField]
    private bool _hasCrosswalks;

    private float _approximateThresholdCorner = 0.3f;

    [SerializeField] private Marker _incomming;

    [SerializeField] private Marker _outgoing;
    public virtual Marker GetpositioForPedestrianToSpwan(Vector3 structurePosition)
    {
        return GetClosestMarkeTo(structurePosition, _pedestrianMarkers);
    }

    public virtual Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        return _outgoing;
    }

    public virtual Marker GetPositioForCarToEnd(Vector3 previousPathPosition)
    {
        return _incomming;
    }

    protected Marker GetClosestMarkeTo(Vector3 structurePosition, List<Marker> pedestrianMarkers, bool isCorner = false)
    {
        if (isCorner)
        {
            foreach (var marker in pedestrianMarkers)
            {
                var direction = marker.GetPosition() - structurePosition;
                direction.Normalize();
                if (Mathf.Abs(direction.x) < _approximateThresholdCorner || Mathf.Abs(direction.z) < _approximateThresholdCorner)
                {
                    return marker;
                }
            }
            return null;
        }
        else
        {
            Marker closestMarker = null;
            float distance = float.MaxValue;
            foreach (var marker in pedestrianMarkers)
            {
                var markerDistance = Vector3.Distance(structurePosition, marker.GetPosition());
                if (distance > markerDistance)
                {
                    distance = markerDistance;
                    closestMarker = marker;
                }
            }
            return closestMarker;
        }
    }

    public Vector3 GetClosestPedestrainPosition(Vector3 currentPosition)
    {
        return GetClosestMarkeTo(currentPosition, _pedestrianMarkers, _isCorner).GetPosition();
    }

    public Vector3 GetClosestCarMarkerPosition(Vector3 currentPosition)
    {
        return GetClosestMarkeTo(currentPosition, _carMarkers, false).GetPosition();
    }


    public List<Marker> GetAllPedestrianMarkers()
    {
        return _pedestrianMarkers;
    }

    public List<Marker> GetAllCarMarkers()
    {
        return _carMarkers;
    }
}