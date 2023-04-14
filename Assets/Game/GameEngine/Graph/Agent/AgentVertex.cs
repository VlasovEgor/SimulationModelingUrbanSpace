using UnityEngine;

public class AgentVertex : Vertex
{
    public AgentVertex cameFromNode = null;

    public AgentVertex(Vector3 position) : base(position)
    {
    }
}
