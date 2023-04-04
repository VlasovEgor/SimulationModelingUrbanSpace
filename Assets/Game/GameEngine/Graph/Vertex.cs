using UnityEngine;

public class Vertex
{
   // public int Number { get; set; }

    public Vector3 Position { get; set; }

    public VertexType VertexType;

    public GameObject Object { get; set; }

    public Vertex(Vector3 position, GameObject gameObject, VertexType vertexType)
    {
        //Number = number;
        VertexType = vertexType;
        Position = position;
        Object = gameObject;
    }
}
