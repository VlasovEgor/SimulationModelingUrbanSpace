using System.Collections.Generic;
using UnityEngine;

public class RoadHelperMultipleCarMarkers : RoadHelper
{
    [SerializeField] private List<Marker> _incommingMarkers = new();
    [SerializeField] private List<Marker> _outgoingMarkers = new();

    public override Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        return GetClosestMarkeTo(nextPathPosition, _outgoingMarkers);
    }

    public override Marker GetPositioForCarToEnd(Vector3 previousPathPosition)
    {
        return GetClosestMarkeTo(previousPathPosition, _incommingMarkers);
    }
}
