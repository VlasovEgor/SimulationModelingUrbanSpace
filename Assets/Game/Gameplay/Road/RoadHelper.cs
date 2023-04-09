using System.Collections.Generic;
using UnityEngine;

public class RoadHelper : MonoBehaviour
{
    [SerializeField] private List<Marker> _pedestrianMarkers;
    [SerializeField] private bool _isCorner;
    [SerializeField] private bool _hasCrosswalks;

    private float _approximateThresholdCorner = 0.3f;

    public Marker GetPositioForPedestrianToSpwan(Vector3 structurePosition)
    {
        return GetClosestMarkerTo(structurePosition);
    }

    public Vector3 GetClosestPedestrainPosition(Vector3 currentPosition)
    {
        return GetClosestMarkerTo(currentPosition, _isCorner).GetPosition();
    }

    private Marker GetClosestMarkerTo(Vector3 structurePosition,  bool isCorner = false)
    {
       // Debug.Log(structurePosition);
        if (isCorner == true)
        {
            Debug.Log("XYI");
            Debug.Log(structurePosition);
            foreach (var marker in _pedestrianMarkers)
            {
                var direction = marker.GetPosition() - structurePosition;
                direction.Normalize();
                if (Mathf.Abs(direction.x) < _approximateThresholdCorner || Mathf.Abs(direction.z) < _approximateThresholdCorner)
                {
                    return marker;
                }
            }
            throw new System.Exception("there are no markers in the vicinity");
        }
        else
        {
           // Debug.Log("PIZDA");
          //  Debug.Log(structurePosition);
            Marker closestMarker = null;
            float distance = float.MaxValue;
            foreach (var marker in _pedestrianMarkers)
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

    public List<Marker> GetAllPedestrianMarkers()
    {
        return _pedestrianMarkers;
    }
}