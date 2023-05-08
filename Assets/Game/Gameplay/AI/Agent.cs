using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum AgentType
{
    HUMAN = 0,
    CAR = 1
}

public class Agent : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<Vector3> OnMove;
    public event Action<bool> ArrivedAtDestination;

    [SerializeField] private Transform _agentTransform;
    [SerializeField] private GameObject _parentGameObject;

    [Space]
    [SerializeField] private AgentType _agentType;

    [Space]
    [SerializeField] private float _arriveDistance = 0.3f;
    [SerializeField] private float _lastpointArriveDistance = 0.1f;

    [Space]
    [ShowInInspector, ReadOnly] private Vector3 _currentTargetPosition;
    [ShowInInspector, ReadOnly] private bool _isMove = false;
    [ShowInInspector, ReadOnly] private List<Vector3> _path;

    [Inject] private CarPool _carPool;
    [Inject] private HumanPool _humanPool;

    private int _index;

    public void SetPath(List<Vector3> path)
    {
        if (path.Count == 0)
        {
            return;
        }

        _path = path;
        _index = 0;

        _currentTargetPosition = _path[_index];
        _parentGameObject.transform.position = _currentTargetPosition;

        SetMove(true);
    }

    private void FixedUpdate()
    {
        CheckIsArrived();
        Move();
    }

    private void CheckIsArrived()
    {
        if (_isMove == true)
        {
            var distanceToCheck = _arriveDistance;

            if (_index == _path.Count - 1)
            {
                distanceToCheck = _lastpointArriveDistance;
            }

            if (Vector3.Distance(_currentTargetPosition, _agentTransform.position) < distanceToCheck)
            {
                SetNextTargetIndex();
            }
        }
    }

    private void Move()
    {
        if (_isMove == true)
        {
            OnMove?.Invoke(_currentTargetPosition);
        }
    }

    private void SetNextTargetIndex()
    {
        _index++;

        if (_index >= _path.Count)
        {
            _isMove = false;
            ArrivedAtDestination?.Invoke(true);
            TurnOff();

        }
        else
        {
            _currentTargetPosition = _path[_index];
        }
    }

    public bool IsThisLastPathIndex()
    {
        return _index >= _path.Count - 1;
    }

    public AgentType GetAgentType()
    {
        return _agentType;
    }

    public void SetMove(bool value)
    {
        _isMove = value;
    }

    public void Created()
    {
        _parentGameObject.SetActive(false);
    }

    public void Init(List<Vector3> path)
    {
        gameObject.SetActive(true);
        _parentGameObject.SetActive(true);
        SetPath(path);
    }

    public void TurnOff()
    {   
        if(GetAgentType() == AgentType.HUMAN)
        {
            _humanPool.Despawn(this);
        }
        else if (GetAgentType() == AgentType.CAR)
        {
            _carPool.Despawn(this);
        }

        _parentGameObject.SetActive(false);
        _path.Clear();
    }

    public class CarPool : MonoMemoryPool<List<Vector3>, Agent>
    {
        protected override void OnCreated(Agent agent)
        {
            agent.Created();
        }

        protected override void Reinitialize(List<Vector3> path, Agent agent)
        {
            agent.Init(path);
        }
    }

    public class HumanPool : MonoMemoryPool<List<Vector3>, Agent>
    {
        protected override void OnCreated(Agent agent)
        {
            agent.Created();
        }

        protected override void Reinitialize(List<Vector3> path, Agent agent)
        {
            agent.Init(path);
        }
    }
}