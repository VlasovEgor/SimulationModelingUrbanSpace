using UnityEngine;

public class Component_GetAgentType : MonoBehaviour, IComponent_GetAgentType
{
    [SerializeField] private Agent _agent;

    public AgentType GetAgentType()
    {
       return _agent.GetAgentType();
    }
}
