//ãîðîäñêîé ãðàô
using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private List<UrbanVertex> _vertices = new();
    private List<Edge> _edges = new();

    public UrbanVertex UrbanVertex
    {
        get => default;
        set
        {
        }
    }

    public Edge Edge
    {
        get => default;
        set
        {
        }
    }

    public void AddVertex(UrbanVertex vertex)
    {
        _vertices.Add(vertex);
    }

    public void RemoveVertex(UrbanVertex vertex)
    {
        var neighbours = GetVerticesList(vertex);

        foreach (var neighbor in neighbours)
        {
            foreach (var edge in _edges)
            {
                if (edge.From == vertex || edge.To == vertex)
                {
                    RemoveEdge(edge);
                }
            }
        }

        _vertices.Remove(vertex);
    }

    public void AddEdge(UrbanVertex from, UrbanVertex to) //non-oriented edge
    {
        var edge = new Edge(from, to);
        _edges.Add(edge);

        var edge2 = new Edge(to, from);
        _edges.Add(edge2);
    }

    public void RemoveEdge(Edge edge)
    {
        _edges.Remove(edge);
    }

    public bool IsVertexByPosition(Vector3 position)
    {
        foreach (var vertex in _vertices)
        {
            var limit = vertex.Object.transform.localScale.x / 2;

            if (Vector3.Distance(position, vertex.Position) <= limit)
            {
                return true;
            }
        }

        return false;
    }

    public UrbanVertex GetVertexByPosition(Vector3 position)
    {
        foreach (var vertex in _vertices)
        {
            if (Vector3.Distance(position, vertex.Position) <= vertex.Object.transform.localScale.x)
            {
                return vertex;
            }
        }

        throw new Exception("there are no vertices at this point");
    }

    public UrbanVertex SearchNearestVertex(Vector3 position)
    {
        float distance = float.MaxValue;
        UrbanVertex nearestVertex = null;

        foreach (var vertex in _vertices)
        {
            if (Vector3.Distance(position, vertex.Position) < distance)
            {
                nearestVertex = vertex;
                distance = Vector3.Distance(position, vertex.Position);
            }
        }

        return nearestVertex;
    }

    public List<UrbanVertex> GetWakableAdjacentVertex(UrbanVertex verteõ)
    {
        List<UrbanVertex> adjacentVertex = GetVerticesList(verteõ);
        for (int i = adjacentVertex.Count - 1; i >= 0; i--)
        {
            if (IsVertexWalkable(adjacentVertex[i].VertexType) == false)
            {
                adjacentVertex.RemoveAt(i);
            }
        }
        return adjacentVertex;
    }

    public List<UrbanVertex> GetVerticesList(UrbanVertex vertex)
    {
        var list = new List<UrbanVertex>();

        foreach (var edge in _edges)
        {
            if (edge.From == vertex)
            {
                list.Add(edge.To);
            }
        }

        return list;
    }

    public List<UrbanVertex> GetAllVerticesOfCertainType(VertexType vertexType)
    {
        List<UrbanVertex> urbanVertices = new();

        foreach (var vertex in _vertices)
        {
            if(vertex.VertexType == vertexType)
            {
                urbanVertices.Add(vertex);
            }
        }

        return urbanVertices;
    }

    public bool IsVertexWalkable(VertexType vertexType)
    {
        return vertexType == VertexType.Road;
    }
}