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

    [Inject] private PlacementManager _placementManager;
    [Inject] private GraphSearch _graphSearch;
    [Inject] private Graph _graph;

    private AgentGraphSearch _agentGraphSearch = new();


    [Button]
    public void SpawnHuman()
    {
        var startPosition = _placementManager.GetRandomBuildingCertainType(VertexType.Residential_Building);
        var endPosition = _placementManager.GetRandomBuildingCertainType(VertexType.Commercial_Building);
        TrySpawningAgent(AgentType.HUMAN, startPosition, endPosition);
    }


    [Button]
    public void SpawnCar()
    {
        var startPosition = _placementManager.GetRandomBuildingCertainType(VertexType.Residential_Building);
        var endPosition = _placementManager.GetRandomBuildingCertainType(VertexType.Commercial_Building);
        TrySpawningAgent(AgentType.CAR, endPosition, startPosition);
    }

    private void TrySpawningAgent(AgentType agentType, BuildingConfig startStructure, BuildingConfig endStructure)
    {
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
                    startPosition = _graph.GetVertexByPosition(startStructure.GetNearestRoad()).Object.GetComponent<RoadHelper>().GetClosestPedestrainPosition(startStructure.GetPosition());
                    endPosition = _graph.GetVertexByPosition(endStructure.GetNearestRoad()).Object.GetComponent<RoadHelper>().GetClosestPedestrainPosition(endStructure.GetPosition());
                }
                else
                {
                    startPosition = _graph.GetVertexByPosition(startRoadPosition).Object.GetComponent<RoadHelper>().GetPositioForCarToSpawn(path[1].Position).GetPosition();
                    endPosition = _graph.GetVertexByPosition(endRoadPosition).Object.GetComponent<RoadHelper>().GetPositioForCarToEnd(path[path.Count - 2].Position).GetPosition();
                }

                List<Vector3> agentPath = GetAgentPath(agentType, path, startPosition, endPosition);

                var agent = Instantiate(GetRandomAgent(agentType), startPosition, Quaternion.identity); //убрать это отсюда
                var positionY = agent.transform.position.y;
                positionY += 2;
                var newPosition = new Vector3(agent.transform.position.x, positionY, agent.transform.position.z);
                agent.transform.position = newPosition;

                agent.GetComponent<UnityEntity>().Get<IComponent_SetPath>().SetPath(agentPath);
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
                        tempDictionary.Add(marker, nextRoadStructure.GetClosestAgentPosition(agentType,marker.GetPosition()));
                    }
                    else
                    {

                        agentGraph.AddEdge(marker.GetPosition(), nextRoadStructure.GetClosestAgentPosition(agentType,marker.GetPosition()));
                    }
                }
            }

            if (limitDistance == true)
            {
                var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.GetPosition(), x.Value)).ToList();
                for (int j = 0; j < 2; j++)
                {

                    agentGraph.AddEdge(distanceSortedMarkers[j].Key.GetPosition(), distanceSortedMarkers[j].Value);
                }
            }
        }

        return agentGraph;
    }
}