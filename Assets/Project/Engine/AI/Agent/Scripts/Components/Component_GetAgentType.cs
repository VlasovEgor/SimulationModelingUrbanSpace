using UnityEngine;

public class Component_GetAgentType : MonoBehaviour, IComponent_GetAgentType
{
    [SerializeField] private Agent _agent;

    public IComponent_GetAgentType IComponent_GetAgentType
    {
        get => default;
        set
        {
        }
    }

    public Agent Agent
    {
        get => default;
        set
        {
        }
    }

    public AgentType GetAgentType()
    {
       return _agent.GetAgentType();
    }
}
