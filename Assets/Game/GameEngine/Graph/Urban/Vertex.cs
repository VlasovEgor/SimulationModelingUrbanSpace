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

    public void ClearValue()
    {
        gCost = float.MaxValue;
        hCost = 0;
        fCost= 0;

    }
}
