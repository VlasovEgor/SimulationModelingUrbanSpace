//класс отвечающий за прокладку дорог
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlacementRoad : MonoBehaviour
{
    public event Action<UrbanVertex> RoadSegmentChanged;
    public event Action<UrbanVertex, UrbanVertex> RoadSegmentsChanged;

    [SerializeField] private Transform _parentTransfrom;

    [Inject] private Graph _graph;

    public Vector3 GetNearestPosition(Vector3 position)
    {
        if (_graph.IsVertexByPosition(position) == true)
        {
            return _graph.GetVertexByPosition(position).Position;
        }
        else
        {
            return position;
        }
    }

    public void PaveRoad(Vector3 startPosition, Vector3 endPosition, GameObject roadStraight)
    {
        if (_graph.IsVertexByPosition(startPosition) == false && _graph.IsVertexByPosition(endPosition) == false)
        {
            PlaceNewRoad(startPosition, endPosition, roadStraight);
        }
        else if (_graph.IsVertexByPosition(startPosition) == true && _graph.IsVertexByPosition(endPosition) == true)
        {
            var firstVertex = _graph.GetVertexByPosition(startPosition);
            var lastVertex = _graph.GetVertexByPosition(endPosition);

            ConnectRoads(firstVertex, lastVertex, roadStraight);
        }
        else if (_graph.IsVertexByPosition(startPosition) == true && _graph.IsVertexByPosition(endPosition) == false)
        {
            var vertex = _graph.GetVertexByPosition(startPosition);
            ContinueRoad(vertex, endPosition, roadStraight);
        }
        else if (_graph.IsVertexByPosition(startPosition) == false && _graph.IsVertexByPosition(endPosition) == true)
        {
            var vertex = _graph.GetVertexByPosition(endPosition);
            ContinueRoad(vertex, startPosition, roadStraight);
        }
    }

    private void ContinueRoad(UrbanVertex currnetSegmentRoad, Vector3 endPosition, GameObject roadStraight)
    {
        // PlaceNewRoad(currnetSegmentRoad.Position, endPosition, roadStraight);
        var segmentsRoadList = CreateRoad(currnetSegmentRoad.Position, endPosition, roadStraight);

        UrbanVertex roadTMP = currnetSegmentRoad;

        for (int i = 0; i < segmentsRoadList.Count; i++)
        {
            var vertexGraph = new UrbanVertex(segmentsRoadList[i].transform.position, segmentsRoadList[i], VertexType.Road);
            _graph.AddVertex(vertexGraph);

            _graph.AddEdge(roadTMP, vertexGraph);
            roadTMP = vertexGraph;
        }

        RoadSegmentChanged?.Invoke(currnetSegmentRoad);
    }

    private void ConnectRoads(UrbanVertex firstSegmentRoad, UrbanVertex secondSegmentRoad, GameObject roadStraight)
    {
        // PlaceNewRoad(firstSegmentRoad.Position, secondSegmentRoad.Position, roadStraight);

        var segmentsRoadList = CreateRoad(firstSegmentRoad.Position, secondSegmentRoad.Position, roadStraight);

        UrbanVertex roadTMP = firstSegmentRoad;

        for (int i = 0; i < segmentsRoadList.Count; i++)
        {
            var vertexGraph = new UrbanVertex(segmentsRoadList[i].transform.position, segmentsRoadList[i], VertexType.Road);
            _graph.AddVertex(vertexGraph);

            _graph.AddEdge(roadTMP, vertexGraph);
            roadTMP = vertexGraph;
        }

        _graph.AddEdge(roadTMP, secondSegmentRoad);

        RoadSegmentsChanged?.Invoke(firstSegmentRoad, secondSegmentRoad);
    }

    private void PlaceNewRoad(Vector3 startPosition, Vector3 endPosition, GameObject roadStraight)
    {
        var segmentsRoadList = CreateRoad(startPosition, endPosition, roadStraight);

        UrbanVertex roadTMP = new UrbanVertex(segmentsRoadList[0].transform.position, segmentsRoadList[0], VertexType.Road);
        _graph.AddVertex(roadTMP);

        for (int i = 1; i < segmentsRoadList.Count; i++)
        {
            var vertexGraph = new UrbanVertex(segmentsRoadList[i].transform.position, segmentsRoadList[i], VertexType.Road);

            _graph.AddVertex(vertexGraph);
            _graph.AddEdge(roadTMP, vertexGraph);

            roadTMP = vertexGraph;
        }

        RoadSegmentChanged?.Invoke(roadTMP);
    }

    private List<GameObject> CreateRoad(Vector3 startPosition, Vector3 endPosition, GameObject roadStraight)
    {
        List<GameObject> segmentsRoadList = new List<GameObject>();

        var roadLenght = Vector3.Distance(startPosition, endPosition);
        var numberSegments = roadLenght / roadStraight.transform.localScale.x;

        var vectorPlacement = endPosition - startPosition;
        var vectorRotate = vectorPlacement;

        vectorPlacement.Normalize();

        for (int i = 0; i < numberSegments; i++)
        {
            var structurePosition = startPosition + vectorPlacement * roadStraight.transform.localScale.x * i;

            if (_graph.IsVertexByPosition(structurePosition) == true)
            {
                continue;
            }

            GameObject newStructure = Instantiate(roadStraight, structurePosition, Quaternion.identity, _parentTransfrom);

            newStructure.transform.forward = vectorRotate;

            segmentsRoadList.Add(newStructure);
        }
        return segmentsRoadList;
    }

    public bool CheckingIfThereNeighbors(UrbanVertex vertex)
    {
        var neighbours = _graph.GetVerticesList(vertex);

        if (neighbours != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<UrbanVertex> GetNeighbourt(UrbanVertex vertex)
    {
        var neighbours = _graph.GetVerticesList(vertex);
        return neighbours;
    }
}