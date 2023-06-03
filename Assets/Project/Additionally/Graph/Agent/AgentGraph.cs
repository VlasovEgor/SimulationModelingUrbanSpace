//Граф перемещения модели человека или автомобиля
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentGraph
{
    Dictionary<AgentVertex, List<AgentVertex>> _adjacencyDictionary = new();

    public AgentVertex AgentVertex
    {
        get => default;
        set
        {
        }
    }

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
        if (_adjacencyDictionary.ContainsKey(v))
            return;
        _adjacencyDictionary.Add(v, new List<AgentVertex>());
    }

    public AgentVertex GetVertexAt(Vector3 position)
    {
        return _adjacencyDictionary.Keys.FirstOrDefault(x => CompareVertices(position, x.Position));
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
        if (_adjacencyDictionary.ContainsKey(v1))
        {
            if (_adjacencyDictionary[v1].FirstOrDefault(x => x == v2) == null)
            {
                _adjacencyDictionary[v1].Add(v2);
            }
        }
        else
        {
            AddVertex(v1);
            _adjacencyDictionary[v1].Add(v2);
        }

    }

    public List<AgentVertex> GetConnectedVerticesTo(AgentVertex v1)
    {
        if (_adjacencyDictionary.ContainsKey(v1))
        {
            return _adjacencyDictionary[v1];
        }
        return null;
    }

    public List<AgentVertex> GetConnectedVerticesTo(Vector3 position)
    {
        var v1 = GetVertexAt(position);
        if (v1 == null)
            return null;
        return _adjacencyDictionary[v1];
    }

    public void ClearGraph()
    {
        _adjacencyDictionary.Clear();
    }

    public IEnumerable<AgentVertex> GetVertices()
    {
        return _adjacencyDictionary.Keys;
    }
}
