//Поиск пути для модели человека или автомобиля с помощью алгоритма А*
using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentGraphSearch
{
    public AgentGraph AgentGraph
    {
        get => default;
        set
        {
        }
    }

    public List<Vector3> AStarSearch(AgentGraph graph, Vector3 startPosition, Vector3 endPosition)
    {
        List<Vector3> path = new();

        AgentVertex start = graph.GetVertexAt(startPosition);
        AgentVertex end = graph.GetVertexAt(endPosition);

        List<AgentVertex> positionsTocheck = new();
        Dictionary<AgentVertex, float> costDictionary = new();
        Dictionary<AgentVertex, float> priorityDictionary = new();
        Dictionary<AgentVertex, AgentVertex> parentsDictionary = new();

        positionsTocheck.Add(start);
        priorityDictionary.Add(start, 0);
        costDictionary.Add(start, 0);
        parentsDictionary.Add(start, null);

        while (positionsTocheck.Count > 0)
        {
            
            AgentVertex current = GetClosestVertex(positionsTocheck, priorityDictionary);
            positionsTocheck.Remove(current);
            if (current.Equals(end))
            {
                path = GeneratePath(parentsDictionary, current);
                return path;
            }

            foreach (AgentVertex neighbour in graph.GetConnectedVerticesTo(current))
            {
                float newCost = costDictionary[current] + 1;
                if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                {
                    costDictionary[neighbour] = newCost;

                    float priority = newCost + ManhattanDiscance(end, neighbour);
                    positionsTocheck.Add(neighbour);;
                    priorityDictionary[neighbour] = priority;

                    parentsDictionary[neighbour] = current;
                }
            }
        }
        return path;
    }

    private AgentVertex GetClosestVertex(List<AgentVertex> list, Dictionary<AgentVertex, float> distanceMap)
    {
        AgentVertex candidate = list[0];
        foreach (AgentVertex vertex in list)
        {
            if (distanceMap[vertex] < distanceMap[candidate])
            {
                candidate = vertex;
            }
        }
        return candidate;
    }

    private float ManhattanDiscance(AgentVertex endPos, AgentVertex position)
    {
        return Math.Abs(endPos.Position.x - position.Position.x) + Math.Abs(endPos.Position.z - position.Position.z);
    }

    public List<Vector3> GeneratePath(Dictionary<AgentVertex, AgentVertex> parentMap, AgentVertex endState)
    {
        List<Vector3> path = new();
        AgentVertex parent = endState;
        while (parent != null && parentMap.ContainsKey(parent))
        {
            path.Add(parent.Position);
            parent = parentMap[parent];
        }
        path.Reverse();
        return path;
    }
}
