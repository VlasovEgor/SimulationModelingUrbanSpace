using System;
using System.Collections.Generic;

public class GraphSearch {

    public List<Vertex> AStarSearch(Graph graph, Vertex startPosition, Vertex endPosition, bool isAgent = false)
    {
        List<Vertex> path = new List<Vertex>();

        List<Vertex> positionsToCheck = new List<Vertex>();
        Dictionary<Vertex, float> costDictionary = new Dictionary<Vertex, float>();
        Dictionary<Vertex, float> priorityDictionary = new Dictionary<Vertex, float>();
        Dictionary<Vertex, Vertex> parentsDictionary = new Dictionary<Vertex, Vertex>();

        positionsToCheck.Add(startPosition);
        priorityDictionary.Add(startPosition, 0);
        costDictionary.Add(startPosition, 0);
        parentsDictionary.Add(startPosition, null);

        while (positionsToCheck.Count > 0)
        {
            Vertex current = GetClosestVertex(positionsToCheck, priorityDictionary);
            positionsToCheck.Remove(current);

            if (current.Equals(endPosition))
            {
                path = GeneratePath(parentsDictionary, current);
                return path;
            }

            foreach (Vertex neighbour in graph.GetWakableAdjacentVertex(current, isAgent))
            {
                float newCost = costDictionary[current] + graph.GetCostOfEnteringVertex(current, neighbour);
                if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                {
                    costDictionary[neighbour] = newCost;

                    float priority = newCost + ManhattanDiscance(endPosition, neighbour);
                    positionsToCheck.Add(neighbour);
                    priorityDictionary[neighbour] = priority;

                    parentsDictionary[neighbour] = current;
                }
            }
        }
        return path;
    }

    private Vertex GetClosestVertex(List<Vertex> list, Dictionary<Vertex, float> distanceMap)
    {
        Vertex candidate = list[0];
        foreach (Vertex vertex in list)
        {
            if (distanceMap[vertex] < distanceMap[candidate])
            {
                candidate = vertex;
            }
        }
        return candidate;
    }

    private float ManhattanDiscance(Vertex endPos, Vertex point)
    {
        return Math.Abs(endPos.Position.x - point.Position.x) + Math.Abs(endPos.Position.y - point.Position.y);
    }

    private List<Vertex> GeneratePath(Dictionary<Vertex, Vertex> parentMap, Vertex endState)
    {
        List<Vertex> path = new List<Vertex>();
        Vertex parent = endState;
        while (parent != null && parentMap.ContainsKey(parent))
        {
            path.Add(parent);
            parent = parentMap[parent];
        }
        return path;
    }
}
