using System.Collections.Generic;
using UnityEngine;

public class Component_SetPath : MonoBehaviour, IComponent_SetPath
{
    [SerializeField] private HumanAgent _humanAgent;

    public void SetPath(List<Vector3> path)
    {
        _humanAgent.SetPath(path);
    }
}
