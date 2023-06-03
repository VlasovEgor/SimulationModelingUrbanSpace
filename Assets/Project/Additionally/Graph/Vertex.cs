//класс описывающий вершину графа
using UnityEngine;

public class Vertex
{
    public Vector3 Position { get; set; }

    public float gCost = float.MaxValue;
    public float hCost;
    public float fCost;

    public Vertex(Vector3 position)
    {
        Position = position;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
