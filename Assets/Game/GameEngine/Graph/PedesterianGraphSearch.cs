using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PedesterianGraphSearch
{
    private PedesterianGraph _graph;

    private List<PedestrianVertex> _openList = new();
    private List<PedestrianVertex> _closedList = new();

    public void SetGraph(PedesterianGraph graph)
    {
        _graph = graph;
    }

    public List<PedestrianVertex> AStar(Vector3 start, Vector3 end)
    {
       // Debug.Log(start);
       // Debug.Log(end);

        var startPosition = _graph.GetVertexAt(start);
        var endPosition = _graph.GetVertexAt(end);

        //Debug.Log(startPosition.Position);
       // Debug.Log(endPosition.Position);
        _openList.Add(startPosition);

        startPosition.gCost = 0;
        startPosition.hCost = CalculateDistance(startPosition, endPosition);
        startPosition.CalculateFCost();

        while (_openList.Count > 0)
        {   

            Debug.Log(_openList.Count);
            PedestrianVertex currentVertex = GetLowestFCostNode(_openList);
            Debug.Log(currentVertex.Position);
            if (currentVertex == endPosition)
            {
                return CalculatePath(endPosition);
            }

            _openList.Remove(currentVertex);
            _closedList.Add(currentVertex);

            var neighbourList = _graph.GetConnectedVerticesTo(currentVertex);
            Debug.Log(neighbourList.Count);
            if(neighbourList.Count == 0)
            {
                Debug.Log(currentVertex.Position);
            }
           // Debug.Log(neighbourList.Count);

            foreach (var neighbourVertex in neighbourList)
            {
                if (_closedList.Contains(neighbourVertex))
                {
                    continue;
                }

                float tentativeGCost = currentVertex.gCost + CalculateDistance(currentVertex, neighbourVertex);

                if (tentativeGCost < neighbourVertex.gCost)
                {
                    neighbourVertex.cameFromNode = currentVertex;
                    neighbourVertex.gCost = tentativeGCost;
                    neighbourVertex.hCost = CalculateDistance(neighbourVertex, endPosition);
                    neighbourVertex.CalculateFCost();

                    if (_openList.Contains(neighbourVertex) == false)
                    {
                        _openList.Add(neighbourVertex);
                    }
                }
            }
        }

        throw new Exception("the path was not found");
    }

    private float CalculateDistance(PedestrianVertex first, PedestrianVertex second)
    {
        return Vector3.Distance(first.Position, second.Position);
    }

    private PedestrianVertex GetLowestFCostNode(List<PedestrianVertex> vertexList)
    {
        PedestrianVertex lowestFCostNode = vertexList[0];

        for (int i = 1; i < vertexList.Count; i++)
        {
            if (vertexList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = vertexList[i];
            }
        }

        return lowestFCostNode;
    }

    private List<PedestrianVertex> CalculatePath(PedestrianVertex endVertex)
    {
        List<PedestrianVertex> path = new() { endVertex };

        PedestrianVertex currentVertex = endVertex;

        while (currentVertex.cameFromNode != null)
        {
            path.Add(currentVertex.cameFromNode);
            currentVertex = currentVertex.cameFromNode;
        }

        return path;
    }
}
