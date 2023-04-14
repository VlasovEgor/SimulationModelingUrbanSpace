using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AgentGraph
{
    Dictionary<AgentVertex, List<AgentVertex>> adjacencyDictionary = new();

    public AgentVertex AddVertex(Vector3 position)
    {
        if (GetVertexAt(position) != null)
        {
            return null;
        }

        AgentVertex v = new(position);
        AddVertex(v);
        return v;

    }

    private void AddVertex(AgentVertex v)
    {
        if (adjacencyDictionary.ContainsKey(v))
            return;
        adjacencyDictionary.Add(v, new List<AgentVertex>());
    }

    public AgentVertex GetVertexAt(Vector3 position)
    {
        return adjacencyDictionary.Keys.FirstOrDefault(x => CompareVertices(position, x.Position));
    }

    private bool CompareVertices(Vector3 position1, Vector3 position2)
    {
        return Vector3.SqrMagnitude(position1 - position2) < 0.0001f;
    }

    public void AddEdge(Vector3 startPosition, Vector3 endPosition)
    {
        if (CompareVertices(startPosition, endPosition))
        {
            return;
        }
        AgentVertex v1 = GetVertexAt(startPosition);
        AgentVertex v2 = GetVertexAt(endPosition);
        if (v1 == null)
        {
            v1 = new AgentVertex(startPosition);
            AddVertex(v1);
        }
        if (v2 == null)
        {
            v2 = new AgentVertex(endPosition);
            AddVertex(v2);
        }
        AddEdgeBetween(v1, v2);
        AddEdgeBetween(v2, v1);

    }

    private void AddEdgeBetween(AgentVertex v1, AgentVertex v2)
    {
        if (v1 == v2)
        {
            return;
        }
        if (adjacencyDictionary.ContainsKey(v1))
        {
            if (adjacencyDictionary[v1].FirstOrDefault(x => x == v2) == null)
            {
                adjacencyDictionary[v1].Add(v2);
            }
        }
        else
        {
            AddVertex(v1);
            adjacencyDictionary[v1].Add(v2);
        }

    }

    public List<AgentVertex> GetConnectedVerticesTo(AgentVertex v1)
    {
        if (adjacencyDictionary.ContainsKey(v1))
        {
            return adjacencyDictionary[v1];
        }
        return null;
    }

    public List<AgentVertex> GetConnectedVerticesTo(Vector3 position)
    {
        var v1 = GetVertexAt(position);
        if (v1 == null)
            return null;
        return adjacencyDictionary[v1];
    }

    public void ClearGraph()
    {
        adjacencyDictionary.Clear();
    }

    public IEnumerable<AgentVertex> GetVertices()
    {
        return adjacencyDictionary.Keys;
    }
}
