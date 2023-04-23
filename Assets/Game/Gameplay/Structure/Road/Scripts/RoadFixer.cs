using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class RoadFixer : MonoBehaviour, IInitializable, IDisposable
{
    [SerializeField] private GameObject _deadEnd;
    [SerializeField] private GameObject _roadStraight;
    [SerializeField] private GameObject _corner;
    [SerializeField] private GameObject _treeWay;
    [SerializeField] private GameObject _fourWay;

    [Space, SerializeField]
    private Transform _parentTransfrom;

    [Inject] private PlacementRoad _placementRoad;

    public void Initialize()
    {
        _placementRoad.RoadSegmentChanged += FixRoadSegment;
        _placementRoad.RoadSegmentsChanged += FixRoadSegments;
    }

    public void Dispose()
    {
        _placementRoad.RoadSegmentChanged -= FixRoadSegment;
        _placementRoad.RoadSegmentsChanged -= FixRoadSegments;
    }

    private void FixRoadSegments(UrbanVertex firstSegment, UrbanVertex secomdSegment)
    {
        FixRoadSegment(firstSegment);
        FixRoadSegment(secomdSegment);
    }

    public void FixRoadSegment(UrbanVertex vertex)
    {
        var neighbourt = _placementRoad.GetNeighbourt(vertex);
        int roadCount = 0;
        roadCount= neighbourt.Where(neighbourt => vertex.VertexType == VertexType.Road).Count(); 

        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(neighbourt, vertex);
        }
        else if (roadCount == 2)
        {   
            if(CreateStraightRoad(neighbourt, vertex))
            {
                return;
            }
            else
            {
                CreateCorner(neighbourt, vertex);
            }
             
        }
        else if (roadCount == 3)
        {
            Create3Way(neighbourt, vertex);
        }
        else if (roadCount == 4)
        {
            Create4Way(vertex);
        }
    }

    public void ModifyRoad(UrbanVertex vertex, GameObject newModel, Vector3 direction)
    {
        var directionRotation = vertex.Position - direction;
        var newSegment = Instantiate(newModel, vertex.Position,Quaternion.LookRotation(directionRotation), _parentTransfrom);

        SwapModel(vertex, newSegment);
    }

    public void SwapModel(UrbanVertex vertex, GameObject model)
    {
        Destroy(vertex.Object);
        vertex.Object= model;
    }

    private void CreateDeadEnd(List<UrbanVertex> neighbourt, UrbanVertex roadSegment)
    {
        Direction[] directions = DeterminePpositionNeighbors(neighbourt, roadSegment);// [0]-up, [1]-right, [2]-down, [3]-left

        if (directions[0].direction == true) //up
        {
            ModifyRoad(roadSegment, _deadEnd, directions[0].position);
        }
        else if (directions[1].direction == true) // right
        {
            ModifyRoad(roadSegment, _deadEnd, directions[1].position);
        }
        else if (directions[2].direction == true) // down
        {
            ModifyRoad(roadSegment, _deadEnd, directions[2].position);
        }
        else if (directions[3].direction == true) // left
        {
            ModifyRoad(roadSegment, _deadEnd, directions[3].position);
        }
    }

    private bool CreateStraightRoad(List<UrbanVertex> neighbourt, UrbanVertex roadSegment)
    {
        Direction[] directions = DeterminePpositionNeighbors(neighbourt, roadSegment);// [0]-up, [1]-right, [2]-down, [3]-left

        if (directions[0].direction == true && directions[2].direction == true)
        {
            ModifyRoad(roadSegment, _corner, directions[1].position);
            return true;
        }
        else if (directions[1].direction == true && directions[3].direction == true)
        {
            ModifyRoad(roadSegment, _corner, directions[2].position);
            return true;
        }

        return false;
    }

    private void CreateCorner(List<UrbanVertex> neighbourt, UrbanVertex roadSegment)
    {
        Direction[] directions = DeterminePpositionNeighbors(neighbourt, roadSegment);// [0]-up, [1]-right, [2]-down, [3]-left

        if (directions[0].direction == true && directions[1].direction == true)
        {
            ModifyRoad(roadSegment, _corner, directions[1].position);
        }
        else if (directions[1].direction == true && directions[2].direction == true)
        {
            ModifyRoad(roadSegment, _corner, directions[2].position);
        }
        else if (directions[2].direction == true && directions[3].direction == true)
        {
            ModifyRoad(roadSegment, _corner, directions[3].position);
        }
        else if (directions[3].direction == true && directions[0].direction == true)
        {
            ModifyRoad(roadSegment, _corner, directions[0].position);
        }
    }

    private void Create3Way(List<UrbanVertex> neighbourt, UrbanVertex roadSegment)
    {
        Direction[] directions = DeterminePpositionNeighbors(neighbourt, roadSegment);// [0]-up, [1]-right, [2]-down, [3]-left

        if (directions[0].direction == true && directions[1].direction == true && directions[3].direction == true) //up
        {
            ModifyRoad(roadSegment, _treeWay, directions[0].position);
        }
        else if (directions[1].direction == true && directions[0].direction == true && directions[2].direction == true) // right
        {
            ModifyRoad(roadSegment, _treeWay, directions[1].position);
        }
        else if (directions[2].direction == true && directions[1].direction == true && directions[3].direction == true) // down
        {
            ModifyRoad(roadSegment, _treeWay, directions[2].position);
        }
        else if (directions[3].direction == true && directions[0].direction == true && directions[2].direction == true) // left
        {
            ModifyRoad(roadSegment, _treeWay, directions[3].position);
        }
    }

    private void Create4Way(UrbanVertex vertex)
    {
        ModifyRoad(vertex, _fourWay, vertex.Object.transform.rotation.ToEuler());
    }

    private Direction[] DeterminePpositionNeighbors(List<UrbanVertex> neighbourt, UrbanVertex vertex)
    {
        Direction[] directions = new Direction[4];// up, right, down,left

        for (int i = 0; i < neighbourt.Count; i++)
        {
            var direction = vertex.Position - neighbourt[i].Position;

            if (neighbourt[i].Position.z > vertex.Position.z && Math.Abs(direction.z) > Math.Abs(direction.x))
            {
                directions[0].direction = true;
                directions[0].position = neighbourt[i].Position;
            }
            else if (neighbourt[i].Position.x > vertex.Position.x && Math.Abs(direction.x) > Math.Abs(direction.z))
            {
                directions[1].direction = true;
                directions[1].position = neighbourt[i].Position;
            }
            else if (neighbourt[i].Position.z < vertex.Position.z && Math.Abs(direction.z) > Math.Abs(direction.x))
            {
                directions[2].direction = true;
                directions[2].position = neighbourt[i].Position;
            }
            else if (neighbourt[i].Position.x < vertex.Position.x && Math.Abs(direction.x) > Math.Abs(direction.z))
            {
                directions[3].direction = true;
                directions[3].position = neighbourt[i].Position;
            }

        }

        return directions;
    }

    struct Direction
    {
        public bool direction;
        public Vector3 position;
    }
}