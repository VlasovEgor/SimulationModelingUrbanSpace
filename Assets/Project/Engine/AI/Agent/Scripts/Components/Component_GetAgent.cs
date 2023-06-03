using UnityEngine;

public class Component_GetAgent : MonoBehaviour, IComponent_GetAgent
{
    [SerializeField] private Agent _agent;

    public IComponent_GetAgent IComponent_GetAgent
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

    public Agent GetAgent()
    {
        return _agent;
    }
}
