using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public event Action OnDeath;
    public event Action<Vector3> OnMove;

   // [SerializeField] private Animator _animator;
    [SerializeField] private Transform _agentTransform;

    [SerializeField] private AgentType _agentType;

    [SerializeField] private List<Vector3> _path;
    [ShowInInspector, ReadOnly] private Vector3 _currentTargetPosition;

    [SerializeField] private float _arriveDistance = 0.3f;
    [SerializeField] private float _lastpointArriveDistance = 0.1f;

    [ShowInInspector, ReadOnly] private bool _isMove = false;

    private int _index;

    public void SetPath(List<Vector3> path)
    {
        if (path.Count == 0)
        {
            //Debug.Log("PRIEXALI_SetPath");
            return;
        }

        _path = path;
        _index = 0;
        _currentTargetPosition = _path[_index];
        SetMove(true);
        //_animator.SetBool("Walk", true);
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
           // _animator.SetBool("Walk", false);
            Debug.Log("PRIEXALI_SetNextTargetIndex");
        }
        else
        {
            _currentTargetPosition = _path[_index];
        }
    }

    public bool IsThisLastPathIndex()
    {
        return _index>= _path.Count-1;
    }

    public AgentType GetAgentType()
    {
        return _agentType;
    }

    public void SetMove(bool value)
    {
        _isMove = value;
    }
}

public enum AgentType
{
    HUMAN = 0,
    CAR = 1
}

