using System;
using UnityEngine;

public class RoadHelperStraight : RoadHelper
{
    [SerializeField] private Marker _leftMarker90;
    [SerializeField] private Marker _rightMarker90;

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public Direction GetDirection(Vector3 direction)
    {
        if (Math.Abs(direction.z) > Math.Abs(direction.x) && direction.z > 0)
        {
           // Debug.Log("UP");
            return Direction.UP;
        }
        else if (Math.Abs(direction.z) > Math.Abs(direction.x) && direction.z < 0)
        {
           // Debug.Log("DOWN");
            return Direction.DOWN;
        }
        else if (Math.Abs(direction.x) > Math.Abs(direction.z) && direction.x > 0)
        {
           // Debug.Log("RIGHT");
            return Direction.RIGHT;
        }
        else
        {
           // Debug.Log("LEFT");
            return Direction.LEFT;
        }
    }


    public override Marker GetPositioForCarToSpawn(Vector3 nextPathPosition)
    {
        var angle = transform.rotation.eulerAngles.y;
       // Debug.Log("Angel : " + angle);
        var direction = nextPathPosition - transform.position;
        return GetCorrextMarker(angle, direction);
    }

    public override Marker GetPositioForCarToEnd(Vector3 previousPathPosition)
    {
        var angle = transform.rotation.eulerAngles.y;
        var direction = transform.position - previousPathPosition;
        return GetCorrextMarker(angle, direction);
    }

    private Marker GetCorrextMarker(float angle, Vector3 directionVerctor)
    {
        var direction = GetDirection(directionVerctor);
       // Debug.Log(angle);


        if (direction == Direction.UP)
        {
            if (90 <= angle && angle < 270)
            {
                return _rightMarker90;
            }
            else
            {
                return _leftMarker90;
            }
        }
        else if (direction == Direction.DOWN)
        {
            if (90 <= angle && angle < 270)
            {
                return _leftMarker90;
            }
            else
            {
                return _rightMarker90;
            }
        }
        else if (direction == Direction.RIGHT)
        {
            if (0 <= angle && angle < 180)
            {
                return _leftMarker90;
            }
            else
            {
                return _rightMarker90;
            }
        }
        else
        {
            if (0 <= angle && angle < 180)
            {
                return _rightMarker90;
            }
            else
            {
                return _leftMarker90;
            }
        }


    }
}