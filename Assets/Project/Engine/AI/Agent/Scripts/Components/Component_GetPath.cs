using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_GetPath : MonoBehaviour, IComponent_GetPath
{
    [SerializeField] private Agent _agent;

    public IComponent_GetPath IComponent_GetPath
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

    public List<Vector3> GetPath()
    {
        return _agent.GetPath();
    }
}
