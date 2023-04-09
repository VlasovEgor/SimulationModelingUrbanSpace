using UnityEngine;

public class PedestrianVertex : Vertex
{
    public PedestrianVertex cameFromNode = null;

    public PedestrianVertex(Vector3 position) : base(position)
    {
    }
}
