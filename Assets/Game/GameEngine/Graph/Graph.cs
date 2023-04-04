using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph 
{   
    private List<Vertex> _vertices = new();
    private List<Edge> _edges = new();

    public int VertexCount 
    { 
        get { return _vertices.Count;} 
    }

    public int EdgeCount 
    { 
        get { return _edges.Count;}
    }

    public void AddVertex(Vertex vertex)
    {
        _vertices.Add(vertex);
    }

    public void RemoveVertex(Vertex vertex) 
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

    public void AddEdge(Vertex from, Vertex to) //non-oriented edge
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

    public Vertex GetVertexByPosition(Vector3 position)
    {
        foreach (var vertex in _vertices)
        {
            if(Vector3.Distance(position,vertex.Position) <= vertex.Object.transform.localScale.x)
            {
                return vertex;
            }
        }

        throw new Exception("there are no vertices at this point");
    }

    public Vertex SearchNearestVertex(Vector3 position)
    {
        float distance = float.MaxValue;
        Vertex nearestVertex = null;

        foreach (var vertex in _vertices)
        {
            if(Vector3.Distance(position,vertex.Position) < distance)
            {
                nearestVertex = vertex;
                distance = Vector3.Distance(position, vertex.Position);
            }
        }

        return nearestVertex;
    }

    public List<Vertex> GetWakableAdjacentVertex(Vertex vertex, bool isAgent)
    {
        List<Vertex> adjacentVertex = GetVerticesList(vertex);
        for (int i = adjacentVertex.Count - 1; i >= 0; i--)
        {
            if (IsVertexWakable(adjacentVertex[i].VertexType, isAgent) == false)
            {
                adjacentVertex.RemoveAt(i);
            }
        }
        return adjacentVertex;
    }

    public List<Vertex> GetVerticesList(Vertex vertex)
    {
        var list = new List<Vertex>();

        foreach (var edge in _edges)
        {
            if (edge.From == vertex)
            {
                list.Add(edge.To);
            }
        }

        return list;
    }

    public bool IsVertexWakable(VertexType vertexType, bool aiAgent = false)
    {
        if (aiAgent)
        {
            return vertexType == VertexType.Road;
        }
        return vertexType == VertexType.Empty || vertexType == VertexType.Road;
    }

    public float GetCostOfEnteringVertex(Vertex current, Vertex neighbour)
    {   
        foreach (var edge in _edges)
        {
            if (edge.From == current && edge.To == neighbour)
            {
                return edge.Weight;
            }
        }

        throw new Exception("there is no edge between these vertices");
    }
}