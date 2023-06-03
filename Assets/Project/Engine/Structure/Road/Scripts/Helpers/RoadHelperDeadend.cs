
using UnityEngine;

public class RoadHelperDeadend : RoadHelper
{
    [SerializeField] private Marker _incomming;
    [SerializeField] private Marker _outgoing;


    public override Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        return _outgoing;
    }

    public override Marker GetPositioForCarToEnd(Vector3 previousPathPosition)
    {
        return _incomming;
    }

}
