
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleCity.AI
{
    public class AiDirectorGovn : MonoBehaviour
    {
        public PlacementManagerGovn placementManager;
        public GameObject[] pedestrianPrefabs;

        AdjacencyGraph graph = new AdjacencyGraph();

        public void SpawnAllAagents()
        {
            foreach (var house in placementManager.GetAllHouses())
            {
                TrySpawningAnAgent(house, placementManager.GetRandomSpecialStrucutre());
            }
            foreach (var specialStructure in placementManager.GetAllSpecialStructures())
            {
                TrySpawningAnAgent(specialStructure, placementManager.GetRandomHouseStructure());
            }
        }

        private void TrySpawningAnAgent(StructureModel startStructure, StructureModel endStructure)
        {
            if(startStructure != null && endStructure != null)
            {
                var startPosition = ((INeedingRoad)startStructure).RoadPosition;
                var endPosition = ((INeedingRoad)endStructure).RoadPosition;

                var startMarkerPosition = Vector3.zero;// placementManager.GetStructureAt(startPosition).GetPedestrianSpawnMarker(startStructure.transform.position);
                var endMarkerPosition = placementManager.GetStructureAt(endPosition).GetNearestMarkerTo(endStructure.transform.position);

                var agent = Instantiate(GetRandomPedestrian(), startMarkerPosition, Quaternion.identity);
                var path = placementManager.GetPathBetween(startPosition, endPosition, true);
                if(path.Count > 0)
                {
                    path.Reverse();
                    List<Vector3> agentPath = GetPedestrianPath(path, startMarkerPosition, endMarkerPosition);
                    var aiAgent = agent.GetComponent<AiAgent>();
                    aiAgent.Initialize(agentPath);
                }
            }
        }

        private List<Vector3> GetPedestrianPath(List<Vector3Int> path, Vector3 startPosition, Vector3 endPosition)
        {
            graph.ClearGraph();
            CreatAGraph(path);
            Debug.Log(graph);
            return AdjacencyGraph.AStarSearch(graph,startPosition,endPosition);
        }

        private void CreatAGraph(List<Vector3Int> path)
        {
            Dictionary<Marker, Vector3> tempDictionary = new Dictionary<Marker, Vector3>();

            for (int i = 0; i < path.Count; i++)
            {
                var currentPosition = path[i];
                var roadStructure = placementManager.GetStructureAt(currentPosition);
                var markersList = roadStructure.GetPedestrianMarkers();
                bool limitDistance = markersList.Count == 4;
                tempDictionary.Clear();
                foreach (var marker in markersList)
                {
                    graph.AddVertex(marker.GetPosition());
                    foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                    {
                       graph.AddEdge(marker.GetPosition(), markerNeighbourPosition);
                    }

                   if(true)//marker.OpenForconnections && i+1 < path.Count)
                   {
                       var nextRoadStructure = placementManager.GetStructureAt(path[i + 1]);
                       if (limitDistance)
                       {
                           tempDictionary.Add(marker, nextRoadStructure.GetNearestMarkerTo(marker.GetPosition()));
                       }
                       else
                       {
                           graph.AddEdge(marker.GetPosition(), nextRoadStructure.GetNearestMarkerTo(marker.GetPosition()));
                       }
                   }
                }
                if(limitDistance && tempDictionary.Count == 4)
                {
                   var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.GetPosition(), x.Value)).ToList();
                    for (int j = 0; j < 2; j++)
                    {
                        graph.AddEdge(distanceSortedMarkers[j].Key.GetPosition(), distanceSortedMarkers[j].Value);
                    }
                }
            }
        }

        private GameObject GetRandomPedestrian()
        {
            return pedestrianPrefabs[UnityEngine.Random.Range(0, pedestrianPrefabs.Length)];
        }

        private void Update()
        {
            foreach (var vertex in graph.GetVertices())
            {
                foreach (var vertexNeighbour in graph.GetConnectedVerticesTo(vertex))
                {
                    Debug.DrawLine(vertex.Position + Vector3.up, vertexNeighbour.Position + Vector3.up, Color.red);
                }
            }
        }

        public void NewMethod(int parameter)
        {
            Debug.Log("hello");
        }

        public void NewMethdTow(string name)
        {
            Debug.Log(name);
        }
    }
}

