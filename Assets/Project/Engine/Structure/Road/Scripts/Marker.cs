using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private bool _openForConnections;
    [SerializeField] private List<Marker> _adjacentMarkers;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public List<Marker> GetAdjacentMarkers()
    {
        return _adjacentMarkers;
    }

    public bool GetOpenForConnections()
    {
        return _openForConnections;
    }

    public List<Vector3> GetAdjacentPositions()
    {
        return new List<Vector3>(_adjacentMarkers.Select(x => x.transform.position).ToList());
    }
}