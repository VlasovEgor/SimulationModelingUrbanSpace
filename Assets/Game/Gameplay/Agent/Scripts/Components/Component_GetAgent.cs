using UnityEngine;

public class Component_GetAgent : MonoBehaviour, IComponent_GetAgent
{
    [SerializeField] private Agent _agent;

    public Agent GetAgent()
    {
        return _agent;
    }
}
