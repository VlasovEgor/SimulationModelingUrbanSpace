using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AgentSpawner
{
    [Inject] private Agent.HumanPool _humanPool;
    [Inject] private Agent.CarPool _carPool;

    private List<Agent> _agnetsHumanPool = new();
    private List<Agent> _agnetsCarPool = new();

    public void AddAgent(AgentType agentType, List<Vector3> agentPath)
    {
        if (agentType == AgentType.HUMAN)
        {
            AddHumanAgent(agentPath);
        }
        else
        {
            AddCarAgent(agentPath);
        }
    }

    public void AddHumanAgent(List<Vector3> path)
    {
        var agent = _humanPool.Spawn(path);
        _agnetsHumanPool.Add(agent);
    }

    public void AddCarAgent(List<Vector3> path)
    {
        var agent = _carPool.Spawn(path);
        _agnetsCarPool.Add(agent);
    }

    public void RemoveHumanAgent()
    {
        var agent = _agnetsHumanPool[0];
        _humanPool.Despawn(agent);
        _agnetsHumanPool.Remove(agent);
    }

    public void RemoveCarAgent()
    {
        var agent = _agnetsCarPool[0];
        _carPool.Despawn(agent);
        _agnetsHumanPool.Remove(agent);
    }

   
}
