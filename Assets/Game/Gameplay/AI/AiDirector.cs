using Entities;
using SimpleCity.AI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class AiDirector : MonoBehaviour
{
    [SerializeField] private GameObject[] _pedestrianPrefabs;
    [SerializeField] private GameObject[] _carPrefab;

    [Inject] private PlacementManager _placementManager;
    [Inject] private GraphSearch _graphSearch;
    [Inject] private Graph _graph;

    private PedesterianGraph _pedestrianGraph = new();
    private PedesterianGraph _carGraph = new();
    private PedesterianGraphSearch _pedesterianGraphSearch = new();

    private List<Vector3> _carPath = new();

    [Button]
    public void SpawnAgent()
    {
        TrySpawningAnHuman(_placementManager.GetRandomBuildingCertainType(VertexType.Residential_Building), _placementManager.GetRandomBuildingCertainType(VertexType.Commercial_Building));
    }

    [Button]
    public void SpawnAllAgents()
    {
        foreach (var residentialBuilding in _placementManager.GetAllBuildingConfigurationCertainType(VertexType.Residential_Building))
        {
            TrySpawningAnHuman(residentialBuilding, _placementManager.GetRandomBuildingCertainType(VertexType.Commercial_Building));
        }
        foreach (var commericalBuilding in _placementManager.GetAllBuildingConfigurationCertainType(VertexType.Commercial_Building))
        {
            TrySpawningAnHuman(commericalBuilding, _placementManager.GetRandomBuildingCertainType(VertexType.Residential_Building));
        }
    }

    private void TrySpawningAnHuman(BuildingConfig startStructure, BuildingConfig endStructure)
    {
        if (startStructure != null && endStructure != null)
        {
            var startPosition = startStructure.GetNearestRoad();
            var endPosition = endStructure.GetNearestRoad();

            var startMarker = _graph.GetVertexByPosition(startStructure.GetNearestRoad()).Object.GetComponent<RoadHelper>().GetClosestPedestrainPosition(startStructure.GetPosition());
            var endMarker = _graph.GetVertexByPosition(endStructure.GetNearestRoad()).Object.GetComponent<RoadHelper>().GetClosestPedestrainPosition(endStructure.GetPosition());

            var agent = Instantiate(GetRandomPedestrian(), startPosition, Quaternion.identity);
            var positionY = agent.transform.position.y;
            positionY += 2;
            var newPosition = new Vector3(agent.transform.position.x, positionY, agent.transform.position.z);
            agent.transform.position = newPosition;

            var path = GetPathBetween(startPosition, endPosition);
            var vectorPath = GetVectorPathBetween(startPosition, endPosition);
            vectorPath.Reverse();

            if (path.Count > 0)
            {
                List<Vector3> agentPath = GetPedestrianPath(path, startMarker, endMarker);
                agent.GetComponent<IComponent_SetPath>().SetPath(agentPath);
            }
        }
    }

    private List<Vector3> GetPedestrianPath(List<UrbanVertex> path, Vector3 startPosition, Vector3 endPosition) //изменил в угоду _pedestrianGraph
    {

        CreatePedestrianGraph(path);

        _pedesterianGraphSearch.SetGraph(_pedestrianGraph);

        //  var PedestrianPath = _pedesterianGraphSearch.AStar(startPosition, endPosition);
        var PedestrianPath = _pedestrianGraph.AStarSearch(_pedestrianGraph, startPosition, endPosition);
        // List<Vector3> VectorPath = new();
        //
        // foreach (PedestrianVertex vertex in PedestrianPath)
        // {
        //     var position = new Vector3(vertex.Position.x, 1f, vertex.Position.z);
        //     VectorPath.Add(position);
        // }
        //

        //VectorPath.Reverse();
        //Debug.Log(PedestrianPath.Count); //0
        return PedestrianPath;
    }

    private void CreatePedestrianGraph(List<UrbanVertex> path)
    {
        Dictionary<Marker, Vector3> tempDictionary = new Dictionary<Marker, Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            var currentPosition = path[i];
            var roadStructure = currentPosition.Object.GetComponent<RoadHelper>();
            var markersList = roadStructure.GetAllPedestrianMarkers();
            bool limitDistance = markersList.Count == 4;
            tempDictionary.Clear();
            foreach (var marker in markersList)
            {
                _pedestrianGraph.AddVertex(marker.GetPosition());
                foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                {
                    _pedestrianGraph.AddEdge(marker.GetPosition(), markerNeighbourPosition);
                }

                if (marker.GetOpenForConnections() && i + 1 < path.Count)
                {
                    var nextRoadStructure = path[i + 1].Object.GetComponent<RoadHelper>();
                    if (limitDistance)
                    {
                        tempDictionary.Add(marker, nextRoadStructure.GetClosestPedestrainPosition(marker.GetPosition()));
                    }
                    else
                    {
                        _pedestrianGraph.AddEdge(marker.GetPosition(), nextRoadStructure.GetClosestPedestrainPosition(marker.GetPosition()));
                    }
                }
            }
            if (limitDistance && tempDictionary.Count == 4)
            {
                var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.GetPosition(), x.Value)).ToList();
                for (int j = 0; j < 2; j++)
                {
                    _pedestrianGraph.AddEdge(distanceSortedMarkers[j].Key.GetPosition(), distanceSortedMarkers[j].Value);
                }
            }

        }

    }

    private GameObject GetRandomPedestrian()
    {
        var index = Random.Range(0, _pedestrianPrefabs.Length - 1);
        return _pedestrianPrefabs[index];
    }

    private GameObject GetRandomCar()
    {
        var index = Random.Range(0, _carPrefab.Length - 1);
        return _carPrefab[index];
    }

    private List<Vector3> GetVectorPathBetween(Vector3 startPosition, Vector3 endPosition)
    {
        var resultPath = GetPathBetween(startPosition, endPosition);

        List<Vector3> path = new();

        foreach (UrbanVertex vertex in resultPath)
        {
            var position = new Vector3(vertex.Position.x, 1f, vertex.Position.z);
            path.Add(position);

        }
        return path;
    }

    private List<UrbanVertex> GetPathBetween(Vector3 startPosition, Vector3 endPosition)
    {
        var resultPath = _graphSearch.AStar(startPosition, endPosition);
        return resultPath;
    }

    private void Update()
    {
        foreach (PedestrianVertex vertex in _pedestrianGraph.GetVertices())
        {
            foreach (var vertexNeighbour in _pedestrianGraph.GetConnectedVerticesTo(vertex))
            {
                Debug.DrawLine(vertex.Position + Vector3.up * 2, vertexNeighbour.Position + Vector3.up * 2, Color.red);
            }
        }

        foreach (PedestrianVertex vertex in _carGraph.GetVertices())
        {
            foreach (var vertexNeighbour in _carGraph.GetConnectedVerticesTo(vertex))
            {
                Debug.DrawLine(vertex.Position + Vector3.up * 2, vertexNeighbour.Position + Vector3.up * 2, Color.white);
            }
        }

        for (int i = 1; i < _carPath.Count; i++)
        {
            Debug.DrawLine(_carPath[i - 1] + Vector3.up * 3, _carPath[i] + Vector3.up * 3, Color.green);
        }


    }



    [Button]
    public void SpawnCar()
    {
        TrySpawnCar(_placementManager.GetRandomBuildingCertainType(VertexType.Residential_Building), _placementManager.GetRandomBuildingCertainType(VertexType.Commercial_Building));
    }

    public void TrySpawnCar(BuildingConfig startPosition, BuildingConfig endPosition)
    {
        if (startPosition != null && endPosition != null)
        {
            var startRoadPosition = startPosition.GetNearestRoad();
            var endRoadPosition = endPosition.GetNearestRoad();

            var path = GetPathBetween(startRoadPosition, endRoadPosition);
            var vectorPath = GetVectorPathBetween(startRoadPosition, endRoadPosition);
            vectorPath.Reverse();

            var startMarkerPosition = _graph.GetVertexByPosition(startRoadPosition).Object.GetComponent<RoadHelper>().GetPositioForCarToSpawn(path[1].Position);
            var endMarkerPosition = _graph.GetVertexByPosition(endRoadPosition).Object.GetComponent<RoadHelper>().GetPositioForCarToEnd(path[path.Count - 2].Position);

            var car = Instantiate(GetRandomCar(), startMarkerPosition.GetPosition(), Quaternion.identity);
            var positionY = car.transform.position.y;
            positionY += 2;
            var newPosition = new Vector3(car.transform.position.x, positionY, car.transform.position.z);
            car.transform.position = newPosition;

            if (path.Count > 0)
            {
                List<Vector3> carPath = GetCarPath(path, startMarkerPosition.GetPosition(), endMarkerPosition.GetPosition());
                _carPath = carPath;
                car.GetComponent<UnityEntity>().Get<IComponent_SetPath>().SetPath(carPath);
            }
        }
    }

    private List<Vector3> GetCarPath(List<UrbanVertex> path, Vector3 startPosition, Vector3 endPosition)
    {
        _carGraph.ClearGraph();
        CreateCarGraph(path);
        return _carGraph.AStarSearch(_carGraph, startPosition, endPosition);
    }

    private void CreateCarGraph(List<UrbanVertex> path)
    {
        Dictionary<Marker, Vector3> tempDictionary = new Dictionary<Marker, Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            var currentPosition = path[i];
            var roadStructure = currentPosition.Object.GetComponent<RoadHelper>();
            var markersList = roadStructure.GetAllCarMarkers();
            bool limitDistance = markersList.Count > 3;
            tempDictionary.Clear();
            int debil = 0;
            foreach (var marker in markersList)
            {
                _carGraph.AddVertex(marker.GetPosition());
                // Debug.Log(marker.GetPosition());
                foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                {
                    _carGraph.AddEdge(marker.GetPosition(), markerNeighbourPosition);
                   // Debug.Log("Vector 1: " + marker.GetPosition());
                  //  Debug.Log("Vector 2: " + markerNeighbourPosition);
                }

                if (marker.GetOpenForConnections() && i + 1 < path.Count)
                {
                    var nextRoadStructure = path[i + 1].Object.GetComponent<RoadHelper>();
                    if (limitDistance == true)
                    {
                        tempDictionary.Add(marker, nextRoadStructure.GetClosestCarMarkerPosition(marker.GetPosition()));
                    }
                    else
                    {
                        debil++;
                        Debug.Log(debil);
                        Debug.Log(marker.GetPosition());
                        _carGraph.AddEdge(marker.GetPosition(), nextRoadStructure.GetClosestCarMarkerPosition(marker.GetPosition()));
                    }
                }
            }

            if (limitDistance == true  && tempDictionary.Count > 1)
            {
                var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.GetPosition(), x.Value)).ToList();
                for (int j = 0; j < 2; j++)
                {
                   // Debug.Log("YEBA" + distanceSortedMarkers[j].Key.GetPosition());
                   // Debug.Log(distanceSortedMarkers[j].Value);
                    _carGraph.AddEdge(distanceSortedMarkers[j].Key.GetPosition(), distanceSortedMarkers[j].Value);
                }
            }
        }

    }

}