using Entities;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AiDirector : MonoBehaviour
{
    [SerializeField] private GameObject[] _pedestrianPrefabs;
    [SerializeField] private GameObject[] _carPrefab;
    [SerializeField] private Transform _parentTransfrom;

    [Inject] private GraphSearch _graphSearch;
    [Inject] private Graph _graph;

    [Inject] private CitizensManager _citizensManager;
    [Inject] private AgentSpawner _agentSpawner;

    private AgentGraphSearch _agentGraphSearch = new();
    private AgentGraph _agentGraph = new();

    [Button]
    public void SendHumanToBuilding(BuidingType buidingType)
    {
        var human = _citizensManager.GetRandomCitizen();

        BuildingConfig startPosition = human.GetPlaceActivity(BuidingType.WORK);
        BuildingConfig endPosition = human.GetPlaceActivity(buidingType);

        if (Vector3.Distance(startPosition.GetPosition(), endPosition.GetPosition()) > 200)
        {
            TrySpawningAgent(AgentType.CAR, startPosition, endPosition);
        }
        else
        {
            TrySpawningAgent(AgentType.HUMAN, startPosition, endPosition);
        }
    }

    private void TrySpawningAgent(AgentType agentType, BuildingConfig startStructure, BuildingConfig endStructure)
    {
        foreach (var vertex in _graph.GetAllVerticesOfCertainType(VertexType.Road))
        {
            vertex.ClearValue();
        }

        if (startStructure != null && endStructure != null)
        {
            var startRoadPosition = startStructure.GetNearestRoad();
            var endRoadPosition = endStructure.GetNearestRoad();

            var path = GetPathBetween(startRoadPosition, endRoadPosition);

            Vector3 startPosition;
            Vector3 endPosition;

            if (path.Count > 0)
            {
                if (agentType == AgentType.HUMAN)
                {
                    startPosition = _graph.GetVertexByPosition(startStructure.GetNearestRoad()).
                        Object.GetComponent<RoadHelper>().GetClosestPedestrainPosition(startStructure.GetPosition());
                    endPosition = _graph.GetVertexByPosition(endStructure.GetNearestRoad()).
                        Object.GetComponent<RoadHelper>().GetClosestPedestrainPosition(endStructure.GetPosition());
                }
                else
                {
                    startPosition = _graph.GetVertexByPosition(startRoadPosition).
                        Object.GetComponent<RoadHelper>().GetPositioForCarToSpawn(path[0].Position).GetPosition();
                    endPosition = _graph.GetVertexByPosition(endRoadPosition).
                        Object.GetComponent<RoadHelper>().GetPositioForCarToEnd(path[path.Count - 1].Position).GetPosition();
                }

                List<Vector3> agentPath = GetAgentPath(agentType, path, startPosition, endPosition);

                _agentSpawner.AddAgent(agentType, agentPath);
            }
        }
    }

    private List<UrbanVertex> GetPathBetween(Vector3 startPosition, Vector3 endPosition)
    {
        var resultPath = _graphSearch.AStar(startPosition, endPosition);
        return resultPath;
    }

    private GameObject GetRandomAgent(AgentType agentType)
    {
        if (agentType == AgentType.HUMAN)
        {
            var index = Random.Range(0, _pedestrianPrefabs.Length - 1);
            return _pedestrianPrefabs[index];
        }
        else
        {
            var index = Random.Range(0, _carPrefab.Length - 1);
            return _carPrefab[index];
        }

    }

    private List<Vector3> GetAgentPath(AgentType agentType, List<UrbanVertex> path, Vector3 startPosition, Vector3 endPosition)
    {
        AgentGraph agentGraph;
        List<Vector3> agnetPath;

        agentGraph = CreateAgentGraph(agentType, path);
        agnetPath = _agentGraphSearch.AStarSearch(agentGraph, startPosition, endPosition);

        return agnetPath;
    }

    private AgentGraph CreateAgentGraph(AgentType agentType, List<UrbanVertex> path)
    {
        Dictionary<Marker, Vector3> tempDictionary = new();
        AgentGraph agentGraph = new();

        for (int i = 0; i < path.Count; i++)
        {
            tempDictionary.Clear();
            var currentPosition = path[i];
            var roadStructure = currentPosition.Object.GetComponent<RoadHelper>();

            List<Marker> markersList = null;
            bool limitDistance = false;

            if (agentType == AgentType.HUMAN)
            {
                markersList = roadStructure.GetAllPedestrianMarkers();
                limitDistance = markersList.Count == 4;

            }
            else
            {
                markersList = roadStructure.GetAllCarMarkers();
                limitDistance = markersList.Count > 3;
            }

            foreach (var marker in markersList)
            {
                agentGraph.AddVertex(marker.GetPosition());

                foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                {
                    agentGraph.AddEdge(marker.GetPosition(), markerNeighbourPosition);

                }

                if (marker.GetOpenForConnections() && i + 1 < path.Count)
                {
                    var nextRoadStructure = path[i + 1].Object.GetComponent<RoadHelper>();

                    if (limitDistance == true)
                    {
                        tempDictionary.Add(marker, nextRoadStructure.GetClosestAgentPosition(agentType, marker.GetPosition()));
                    }
                    else
                    {
                        agentGraph.AddEdge(marker.GetPosition(), nextRoadStructure.GetClosestAgentPosition(agentType, marker.GetPosition()));
                    }
                }
            }

            if (limitDistance == true && tempDictionary.Count > 2)
            {
                var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.GetPosition(), x.Value)).ToList();
                for (int j = 0; j < 2; j++)
                {

                    agentGraph.AddEdge(distanceSortedMarkers[j].Key.GetPosition(), distanceSortedMarkers[j].Value);
                }
            }
        }

        _agentGraph = agentGraph;
        return agentGraph;
    }

    private void Update()
    {
        if (_agentGraph != null)
        {
            DrawCraphPath();
        }
    }

    private void DrawCraphPath()
    {
        foreach (var vertex in _agentGraph.GetVertices())
        {
            foreach (var vertexNeighbour in _agentGraph.GetConnectedVerticesTo(vertex))
            {
                Debug.DrawLine(vertex.Position + Vector3.up * 2, vertexNeighbour.Position + Vector3.up * 2, Color.red);
            }
        }
    }
}