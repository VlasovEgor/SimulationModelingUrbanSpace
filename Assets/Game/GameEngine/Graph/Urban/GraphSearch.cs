using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GraphSearch
{
    [Inject] private Graph _graph;

    public List<UrbanVertex> AStar(Vector3 start, Vector3 end)
    {
        List<UrbanVertex> openList = new();
        List<UrbanVertex> closedList = new();

        var startPosition = _graph.GetVertexByPosition(start);
        var endPosition = _graph.GetVertexByPosition(end);

        openList.Add(startPosition);

        startPosition.gCost = 0;
        startPosition.hCost = CalculateDistance(startPosition, endPosition);
        startPosition.CalculateFCost();

        while (openList.Count > 0)
        {   
            UrbanVertex currentVertex = GetLowestFCostNode(openList);
            if (currentVertex == endPosition)
            {
                return CalculatePath(endPosition);
            }

            openList.Remove(currentVertex);
            closedList.Add(currentVertex);

            var neighbourList = _graph.GetWakableAdjacentVertex(currentVertex);

            foreach (var neighbourVertex in neighbourList)
            {
                if (closedList.Contains(neighbourVertex))
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

                    if (openList.Contains(neighbourVertex) == false)
                    {
                        openList.Add(neighbourVertex);
                    }
                }
            }
        }
        
        ClearVerticesValue(openList);
        ClearVerticesValue(closedList);
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

        ClearVerticesValue(path);
        return path;
    }

    private void ClearVerticesValue(List<UrbanVertex> urbanVertices)
    {
       foreach (var vertex in urbanVertices)
       {
           vertex.ClearValue();
       }
    }
}
