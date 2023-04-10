
using Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SmartCrosswalks : MonoBehaviour
{
    public event Action<bool> OnPedestrianEnter;
    public event Action<bool> OnPedestrianExit;

    [SerializeField] private SmartRoad _smartRoad;
    private List<Agent> _pedestrianList = new();

    private void Start()
    {
        _smartRoad.OnPedestrianCanWalk += MovePedestrians;
    }

    private void OnDestroy()
    {
        _smartRoad.OnPedestrianCanWalk -= MovePedestrians;
    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.GetComponent<Entity>().Get<IComponent_GetAgentType>().GetAgentType() == AgentType.HUMAN)
        {
            var agent = other.gameObject.GetComponent<Entity>().Get<IComponent_GetAgent>().GetAgent();
            if(agent != null && _pedestrianList.Contains(agent) == false)
            {
                _pedestrianList.Add(agent);
                agent.SetMove(false);
                OnPedestrianEnter?.Invoke(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>().Get<IComponent_GetAgentType>().GetAgentType() == AgentType.HUMAN)
        {
            var agent = other.gameObject.GetComponent<Entity>().Get<IComponent_GetAgent>().GetAgent();
            if (agent != null )
            {
                _pedestrianList.Remove(agent);

                if(_pedestrianList.Count <=0)
                {
                    OnPedestrianExit?.Invoke(false);
                }
            }
        }
    }

    public void MovePedestrians()
    {
        foreach (var pedestrian in _pedestrianList)
        {
            pedestrian.SetMove(true);
        }
    }
}
