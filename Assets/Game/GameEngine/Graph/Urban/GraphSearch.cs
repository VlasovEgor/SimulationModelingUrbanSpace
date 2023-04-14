using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GraphSearch {

    [Inject] private Graph _graph;

    private List<UrbanVertex> _openList =new();
    private List<UrbanVertex> _closedList = new();

    public List<UrbanVertex> AStar(Vector3 start, Vector3 end)
    {
        var startPosition = _graph.GetVertexByPosition(start);
        var endPosition = _graph.GetVertexByPosition(end);

        _openList.Add(startPosition);

        startPosition.gCost = 0;
        startPosition.hCost = CalculateDistance(startPosition, endPosition);
        startPosition.CalculateFCost();

        while (_openList.Count > 0)
        {
            UrbanVertex currentVertex = GetLowestFCostNode(_openList);

            if(currentVertex == endPosition)
            {
                return CalculatePath(endPosition);
            }

            _openList.Remove(currentVertex);
            _closedList.Add(currentVertex);

           var neighbourList = _graph.GetWakableAdjacentVertex(currentVertex);

            foreach (var neighbourVertex in neighbourList)
            {
                if(_closedList.Contains(neighbourVertex))
                {
                    continue;
                }

                float tentativeGCost = currentVertex.gCost + CalculateDistance(currentVertex, neighbourVertex);

                if(tentativeGCost < neighbourVertex.gCost)
                {
                    neighbourVertex.cameFromNode = currentVertex;
                    neighbourVertex.gCost = tentativeGCost;
                    neighbourVertex.hCost = CalculateDistance(neighbourVertex, endPosition);
                    neighbourVertex.CalculateFCost();

                    if(_openList.Contains(neighbourVertex) == false)
                    {
                        _openList.Add(neighbourVertex);
                    }
                }
            }
        }

        throw new Exception("the path was not found");
    }

    private float CalculateDistance(UrbanVertex first, UrbanVertex second)
    {
        return Vector3.Distance(first.Position, second.Position);
    }

    private UrbanVertex GetLowestFCostNode(List<UrbanVertex> vertexList)
    {
        UrbanVertex lowestFCostNode = vertexList[0];

        for (int i = 1; i < vertexList.Count; i++)
        {
            if (vertexList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = vertexList[i];
            }
        }

        return lowestFCostNode;
    }

    private List<UrbanVertex> CalculatePath(UrbanVertex endVertex)
    {
        List<UrbanVertex> path = new() { endVertex };

        UrbanVertex currentVertex = endVertex;

        while (currentVertex.cameFromNode != null)
        {
            path.Add(currentVertex.cameFromNode);
            currentVertex = currentVertex.cameFromNode;
        }

        return path;
    }
}
