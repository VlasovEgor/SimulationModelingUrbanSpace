using UnityEngine;

public class UrbanVertex : Vertex
{
    // public int Number { get; set; }

    public VertexType VertexType;

    public GameObject Object { get; set; }

    public UrbanVertex cameFromNode = null;

    public UrbanVertex(Vector3 position, GameObject gameObject, VertexType vertexType) : base(position)
    {
        //Number = number;
        VertexType = vertexType;
        Position = position;
        Object = gameObject;
    }
}
