using SimpleCity.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PedesterianGraph
{
    // private List<PedestrianVertex> _vertices = new();
    // private List<PedesterianEdge> _edges = new();
    //
    // public int CountVertices
    // {
    //     get { return _vertices.Count; }
    // }
    //
    // public int CountEdges
    // {
    //     get { return _edges.Count; }
    // }
    //
    // public void AddVertex(PedestrianVertex vertex)
    // {
    //     _vertices.Add(vertex);
    // }
    //
    // public void RemoveVertex(PedestrianVertex vertex)
    // {
    //     var neighbours = GetVerticesList(vertex);
    //
    //     foreach (var neighbor in neighbours)
    //     {
    //         foreach (var edge in _edges)
    //         {
    //             if (edge.From == vertex || edge.To == vertex)
    //             {
    //                 RemoveEdge(edge);
    //             }
    //         }
    //     }
    //
    //     _vertices.Remove(vertex);
    // }
    //
    // public void AddEdge(PedestrianVertex from, PedestrianVertex to) //non-oriented edge
    // {
    //     var edge = new PedesterianEdge(from, to);
    //     _edges.Add(edge);
    //
    //     var edge2 = new PedesterianEdge(to, from);
    //     _edges.Add(edge2);
    // }
    //
    // public void RemoveEdge(PedesterianEdge edge)
    // {
    //     _edges.Remove(edge);
    // }
    //
    // public List<PedestrianVertex> GetVerticesList(PedestrianVertex vertex)
    // {
    //     var list = new List<PedestrianVertex>();
    //
    //     foreach (var edge in _edges)
    //     {
    //        // Debug.Log("XYI");
    //         if (edge.From == vertex)
    //         {
    //             list.Add(edge.To);
    //         }
    //     }
    //     Debug.Log(list.Count);
    //     return list;
    // }
    //
    // public List<PedestrianVertex> GetAllVertices()
    // {
    //     return _vertices;
    // }
    //
    // public List<PedestrianVertex> GetConnectedVerticesTo(PedestrianVertex vertex)
    // {
    //     var list = new List<PedestrianVertex>();
    //
    //     foreach (var edge in _edges)
    //     {
    //         if (edge.From == vertex)
    //         {
    //             list.Add(edge.To);
    //         }
    //     }
    //
    //     return list;
    // }
    //
    // public PedestrianVertex GetVertexByPosition(Vector3 position)
    // {
    //     foreach (var vertex in _vertices)
    //     {
    //         if (Vector3.Distance(position, vertex.Position) <= 0.1)
    //         {
    //             return vertex;
    //         }
    //     }
    //
    //     throw new Exception("there are no vertices at this point");
    // }

    Dictionary<PedestrianVertex, List<PedestrianVertex>> adjacencyDictionary = new();

    public PedestrianVertex AddVertex(Vector3 position)
    {
        if (GetVertexAt(position) != null)
        {
            return null;
        }

        PedestrianVertex v = new(position);
        AddVertex(v);
        return v;

    }

    private void AddVertex(PedestrianVertex v)
    {
        if (adjacencyDictionary.ContainsKey(v))
            return;
        adjacencyDictionary.Add(v, new List<PedestrianVertex>());
    }

    public PedestrianVertex GetVertexAt(Vector3 position)
    {
        return adjacencyDictionary.Keys.FirstOrDefault(x => CompareVertices(position, x.Position));
    }

    private bool CompareVertices(Vector3 position1, Vector3 position2)
    {
        return Vector3.SqrMagnitude(position1 - position2) < 0.0001f;
    }

    public void AddEdge(Vector3 position1, Vector3 position2)
    {
        if (CompareVertices(position1, position2))
        {
            return;
        }
        PedestrianVertex v1 = GetVertexAt(position1);
        PedestrianVertex v2 = GetVertexAt(position2);
        if (v1 == null)
        {
            v1 = new PedestrianVertex(position1);
        }
        if (v2 == null)
        {
            v2 = new PedestrianVertex(position2);
        }
        AddEdgeBetween(v1, v2);
        AddEdgeBetween(v2, v1);

    }

    private void AddEdgeBetween(PedestrianVertex v1, PedestrianVertex v2)
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

    public List<PedestrianVertex> GetConnectedVerticesTo(PedestrianVertex v1)
    {
        if (adjacencyDictionary.ContainsKey(v1))
        {
            return adjacencyDictionary[v1];
        }
        return null;
    }

    public List<PedestrianVertex> GetConnectedVerticesTo(Vector3 position)
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

    public IEnumerable<Vertex> GetVertices()
    {
        return adjacencyDictionary.Keys;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder();
        foreach (var vertex in adjacencyDictionary.Keys)
        {
            builder.AppendLine("Vertex " + vertex.ToString() + " neighbours: " + String.Join(", ", adjacencyDictionary[vertex]));
        }
        return builder.ToString();
    }

    public List<Vector3> AStarSearch(PedesterianGraph graph, Vector3 startPosition, Vector3 endPosition)
    {
        List<Vector3> path = new List<Vector3>();

        PedestrianVertex start = graph.GetVertexAt(startPosition);
        PedestrianVertex end = graph.GetVertexAt(endPosition);
        Debug.Log("END: "+end.Position);

        List<PedestrianVertex> positionsTocheck = new List<PedestrianVertex>();
        Dictionary<PedestrianVertex, float> costDictionary = new Dictionary<PedestrianVertex, float>();
        Dictionary<PedestrianVertex, float> priorityDictionary = new Dictionary<PedestrianVertex, float>();
        Dictionary<PedestrianVertex, PedestrianVertex> parentsDictionary = new Dictionary<PedestrianVertex, PedestrianVertex>();

        positionsTocheck.Add(start);
        priorityDictionary.Add(start, 0);
        costDictionary.Add(start, 0);
        parentsDictionary.Add(start, null);

        while (positionsTocheck.Count > 0)
        {   
            Debug.Log(positionsTocheck.Count);
            PedestrianVertex current = GetClosestVertex(positionsTocheck, priorityDictionary);
            positionsTocheck.Remove(current);
            if (current.Equals(end))
            {
                Debug.Log("XYI");
                path = GeneratePath(parentsDictionary, current);
                return path;
            }
           // Debug.Log(graph.GetConnectedVerticesTo(current).Count);
            foreach (PedestrianVertex neighbour in graph.GetConnectedVerticesTo(current))
            {
                float newCost = costDictionary[current] + 1;
                if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                {
                    Debug.Log("ABOBA");
                    costDictionary[neighbour] = newCost;

                    float priority = newCost + ManhattanDiscance(end, neighbour);
                    positionsTocheck.Add(neighbour);
                    //Debug.Log(positionsTocheck.Count);
                    priorityDictionary[neighbour] = priority;

                    parentsDictionary[neighbour] = current;
                    Debug.Log(current.Position);
                }
            }
        }
        return path;
    }

    private PedestrianVertex GetClosestVertex(List<PedestrianVertex> list, Dictionary<PedestrianVertex, float> distanceMap)
    {
        PedestrianVertex candidate = list[0];
        foreach (PedestrianVertex vertex in list)
        {
            if (distanceMap[vertex] < distanceMap[candidate])
            {
                candidate = vertex;
            }
        }
        return candidate;
    }

    private float ManhattanDiscance(PedestrianVertex endPos, PedestrianVertex position)
    {
        return Math.Abs(endPos.Position.x - position.Position.x) + Math.Abs(endPos.Position.z - position.Position.z);
    }

    public List<Vector3> GeneratePath(Dictionary<PedestrianVertex, PedestrianVertex> parentMap, PedestrianVertex endState)
    {
        List<Vector3> path = new List<Vector3>();
        PedestrianVertex parent = endState;
        while (parent != null && parentMap.ContainsKey(parent))
        {
            path.Add(parent.Position);
            parent = parentMap[parent];
        }
        path.Reverse();
        return path;
    }
}
