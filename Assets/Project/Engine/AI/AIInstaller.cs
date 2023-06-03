
using UnityEngine;
using Zenject;

public class AIInstaller : MonoInstaller
{
    [SerializeField] private GameObject _humanPrefab;
    [SerializeField] private GameObject _carPrefab;

    [SerializeField] private Transform _humanTransform;
    [SerializeField] private Transform _carTransform;

    [SerializeField] private AgentPath _agentPath;

    public AgentsController AgentsController
    {
        get => default;
        set
        {
        }
    }

    public AgentSpawner AgentSpawner
    {
        get => default;
        set
        {
        }
    }

    public AgentPath AgentPath
    {
        get => default;
        set
        {
        }
    }

    public override void InstallBindings()
    {
        BindAgentsPool();
        BindAgentSpawner();
        BindAgentsManager();
        BindAgentPath();
        BindAgentsController();
    }

    private void BindAgentsPool()
    {
        Container.BindMemoryPool<Agent, Agent.HumanPool>().
            WithInitialSize(300).
            ExpandByOneAtATime().
            FromComponentInNewPrefab(_humanPrefab).
            UnderTransform(_humanTransform);

        Container.BindMemoryPool<Agent, Agent.CarPool>().
          WithInitialSize(300).
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

    private void BindAgentsController()
    {
        Container.BindInterfacesAndSelfTo<AgentsController>().
              AsSingle();
    }
}
