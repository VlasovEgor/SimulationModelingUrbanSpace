using System.Collections.Generic;
using UnityEngine;

public class Component_SetPath : MonoBehaviour, IComponent_SetPath
{
    [SerializeField] private Agent _humanAgent;

    public IComponent_SetPath IComponent_SetPath
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

    public void SetPath(List<Vector3> path)
    {
        _humanAgent.SetPath(path);
    }
}
