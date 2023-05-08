
using UnityEngine;
using Zenject;

public class AIInstaller : MonoInstaller
{
    [SerializeField] private GameObject _humanPrefab;
    [SerializeField] private GameObject _carPrefab;

    [SerializeField] private Transform _humanTransform;
    [SerializeField] private Transform _carTransform;

    [SerializeField] private AgentPath _agentPath;

    public override void InstallBindings()
    {
        BindAgentsPool();
        BindAgentSpawner();
        BindAgentsManager();
        BindAgentPath();
    }

    private void BindAgentsPool()
    {
        Container.BindMemoryPool<Agent, Agent.HumanPool>().
            WithInitialSize(10).
            ExpandByOneAtATime().
            FromComponentInNewPrefab(_humanPrefab).
            UnderTransform(_humanTransform);

        Container.BindMemoryPool<Agent, Agent.CarPool>().
          WithInitialSize(10).
          ExpandByOneAtATime().
          FromComponentInNewPrefab(_carPrefab).
          UnderTransform(_carTransform);
    }

    private void BindAgentSpawner()
    {
        Container.Bind<AgentSpawner>().
            AsSingle();
    }

    private void BindAgentsManager()
    {
        Container.BindInterfacesAndSelfTo<TimeManager>().
            AsSingle();
    }

    private void BindAgentPath()
    {
        Container.BindInterfacesAndSelfTo<AgentPath>().
            FromInstance(_agentPath).
            AsSingle();
    }
}
