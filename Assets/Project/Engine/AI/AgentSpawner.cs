using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AgentSpawner
{
    [Inject] private Agent.HumanPool _humanPool;
    [Inject] private Agent.CarPool _carPool;

    public Agent AddAgent(AgentType agentType, List<Vector3> agentPath)
    {
        Agent agent;

        if (agentType == AgentType.HUMAN)
        {
            agent = AddHumanAgent(agentPath);
        }
        else
        {
            agent = AddCarAgent(agentPath);
        }

        return agent;
    }

    private Agent AddHumanAgent(List<Vector3> path)
    {
        var agent = _humanPool.Spawn(path);

        return agent;
    }

    private Agent AddCarAgent(List<Vector3> path)
    {
        var agent = _carPool.Spawn(path);

        return agent;
    }
}
